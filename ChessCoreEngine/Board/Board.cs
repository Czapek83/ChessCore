using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ChessCoreEngine.Tests")]

namespace ChessEngine.Engine
{
    public sealed class Board
    {
        internal Square[] Squares { get; private set; }
        internal bool[] BlackAttackBoard { get; private set; }
        internal bool[] WhiteAttackBoard { get; private set; }

        internal ulong ZobristHash { get; private set; }
        internal bool IsDraw
        {
            get
            {
                if (InsufficientMaterial || RepeatedMove >= 3 || FiftyMove >= 50)
                    return true;
                return false;
            }
        }

        internal bool InsufficientMaterial
        {
            get
            {
                var pieces = Squares.Where(y => y.Piece != null).Select(z => z.Piece);

                if (pieces.Any(x => x.PieceType != ChessPieceType.Bishop 
                && x.PieceType != ChessPieceType.Knight && x.PieceType != ChessPieceType.King))
                    return false;

                if (AreTwoKnightsOrLessOnly() || AreTwoBishopsOppositeOrOneBishopOnly())
                    return true;

                bool AreTwoKnightsOrLessOnly()
                {
                    return pieces.Count(x => x.PieceType == ChessPieceType.Knight) <= 2
                        && !pieces.Any(y => y.PieceType != ChessPieceType.Knight && y.PieceType != ChessPieceType.King);
                }

                bool AreTwoBishopsOppositeOrOneBishopOnly()
                {
                    return pieces.Count(x => x.PieceType == ChessPieceType.Bishop && x.PieceColor == ChessPieceColor.White) <= 1
                        && pieces.Count(x => x.PieceType == ChessPieceType.Bishop && x.PieceColor == ChessPieceColor.Black) <= 1
                        && !pieces.Any(y => y.PieceType != ChessPieceType.Bishop && y.PieceType != ChessPieceType.King);
                }

                return false;
            }
        }
        internal bool EndGamePhase
        {
            get
            {
                return Squares.Count(x => x.Piece != null) < 10;
            }
        }

        internal byte WhiteKingPosition
        {
            get
            {
                return (byte)new List<Square>(Squares).IndexOf(GetKingSquare(ChessPieceColor.White));
            }
        }
        internal byte BlackKingPosition
        {
            get
            {
                return (byte)new List<Square>(Squares).IndexOf(GetKingSquare(ChessPieceColor.Black));
            }
        }

        internal byte FiftyMove { get; private set; }

        internal int Score;
        
        //Game Over Flags
        internal bool BlackCheck;
        internal bool BlackMate;
        internal bool WhiteCheck;
        internal bool WhiteMate;

        internal byte RepeatedMove;

        internal bool BlackCastled;
        internal bool WhiteCastled;

        internal bool BlackCanCastle;
        internal bool WhiteCanCastle;

        
        internal MoveContent LastMove;

        //Who initated En Passant
        internal ChessPieceColor EnPassantColor;
        //Positions liable to En Passant
        internal byte EnPassantPosition;

        internal ChessPieceColor WhoseMove;
        
        internal int MoveCount;

        #region Constructors

        internal Board()
        {
            Squares = new Square[64];

            for (byte i = 0; i < 64; i++)
            {
                Squares[i] = new Square();
            }

            LastMove = new MoveContent();

            BlackCanCastle = true;
            WhiteCanCastle = true;

            WhiteAttackBoard = new bool[64];
            BlackAttackBoard = new bool[64];
        }

        private Board(Square[] squares)
        {
            Squares = new Square[64];

            for (byte x = 0; x < 64; x++)
            {
                if (squares[x].Piece != null)
                {
                    Squares[x].Piece = new Piece(squares[x].Piece);
                }
            }

            

            WhiteAttackBoard = new bool[64];
            BlackAttackBoard = new bool[64];

        }

        //Copy Constructor
        internal Board(Board board)
        {
            Squares = new Square[64];

            for (byte x = 0; x < 64; x++)
            {
                if (board.Squares[x].Piece != null)
                {
                    Squares[x] = new Square(board.Squares[x].Piece);
                }
            }

            WhiteAttackBoard = new bool[64];
            BlackAttackBoard = new bool[64];

            for (byte x = 0; x < 64; x++)
            {
                WhiteAttackBoard[x] = board.WhiteAttackBoard[x];
                BlackAttackBoard[x] = board.BlackAttackBoard[x];
            }

            FiftyMove = board.FiftyMove;
            RepeatedMove = board.RepeatedMove;
           
            WhiteCastled = board.WhiteCastled;
            BlackCastled = board.BlackCastled;

            WhiteCanCastle = board.WhiteCanCastle;
            BlackCanCastle = board.BlackCanCastle;

            BlackCheck = board.BlackCheck;
            WhiteCheck = board.WhiteCheck;
            WhiteMate = board.WhiteMate;
            BlackMate = board.BlackMate;
            WhoseMove = board.WhoseMove;
            EnPassantPosition = board.EnPassantPosition;
            EnPassantColor = board.EnPassantColor;

            ZobristHash = board.ZobristHash;

            Score = board.Score;

            LastMove = new MoveContent(board.LastMove);

            MoveCount = board.MoveCount;
        }

        #endregion

        #region PrivateMethods

        private static bool PromotePawns(Board board, Piece piece, byte dstPosition, ChessPieceType promoteToPiece)
        {
            if (piece.PieceType == ChessPieceType.Pawn)
            {
                if (dstPosition < 8)
                {
                    board.Squares[dstPosition].Piece.PieceType = promoteToPiece;
                    board.Squares[dstPosition].Piece.PieceValue = Piece.CalculatePieceValue(promoteToPiece);
                    board.Squares[dstPosition].Piece.PieceActionValue = Piece.CalculatePieceActionValue(promoteToPiece);
                    return true;
                }
                if (dstPosition > 55)
                {
                    board.Squares[dstPosition].Piece.PieceType = promoteToPiece;
                    board.Squares[dstPosition].Piece.PieceValue = Piece.CalculatePieceValue(promoteToPiece);
                    board.Squares[dstPosition].Piece.PieceActionValue = Piece.CalculatePieceActionValue(promoteToPiece);
                    return true;
                }
            }

            return false;
        }

        private static void RecordEnPassant(ChessPieceColor pcColor, ChessPieceType pcType, Board board, byte srcPosition, byte dstPosition)
        {
            //Record En Passant if Pawn Moving
            if (pcType == ChessPieceType.Pawn)
            {
                //Reset FiftyMoveCount if pawn moved
                board.FiftyMove = 0;

                int difference = srcPosition - dstPosition; 

                if (difference == 16 || difference == -16)
                {
                    board.EnPassantPosition = (byte)(dstPosition + (difference / 2));
                    board.EnPassantColor = pcColor;
                }
            }
        }

        private static bool SetEnpassantMove(Board board, byte srcPosition, byte dstPosition, ChessPieceColor pcColor)
        {
            if (board.EnPassantPosition != dstPosition)
            {
                return false;
            }

            if (pcColor == board.EnPassantColor)
            {
                return false;
            }

            if (board.Squares[srcPosition].Piece.PieceType != ChessPieceType.Pawn)
            {
                return false;
            }

            int pieceLocationOffset = 8;

            if (board.EnPassantColor == ChessPieceColor.White)
            {
                pieceLocationOffset = -8;
            }

            dstPosition = (byte)(dstPosition + pieceLocationOffset);

            Square sqr = board.Squares[dstPosition];

            board.LastMove.TakenPiece = new PieceTaken(sqr.Piece.PieceColor, sqr.Piece.PieceType, sqr.Piece.Moved, dstPosition);

            board.Squares[dstPosition].Piece = null;
                    
            //Reset FiftyMoveCount if capture
            board.FiftyMove = 0;

            return true;

        }

        private static void KingCastle(Board board, Piece piece, byte srcPosition, byte dstPosition)
        {
            if (piece.PieceType != ChessPieceType.King)
            {
                return;
            }

            //Lets see if this is a casteling move.
            if (piece.PieceColor == ChessPieceColor.White && srcPosition == 60)
            {
                //Castle Right
                if (dstPosition == 62)
                {
                    //Ok we are casteling we need to move the Rook
                    if (board.Squares[63].Piece != null)
                    {
                        board.Squares[61].Piece = board.Squares[63].Piece;
                        board.Squares[63].Piece = null;
                        board.WhiteCastled = true;
                        board.LastMove.MovingPieceSecondary = new PieceMoving(board.Squares[61].Piece.PieceColor, board.Squares[61].Piece.PieceType, board.Squares[61].Piece.Moved, 63, 61);
                        board.Squares[61].Piece.Moved = true;
                        return;
                    }
                }
                //Castle Left
                else if (dstPosition == 58)
                {   
                    //Ok we are casteling we need to move the Rook
                    if (board.Squares[56].Piece != null)
                    {
                        board.Squares[59].Piece = board.Squares[56].Piece;
                        board.Squares[56].Piece = null;
                        board.WhiteCastled = true;
                        board.LastMove.MovingPieceSecondary = new PieceMoving(board.Squares[59].Piece.PieceColor, board.Squares[59].Piece.PieceType, board.Squares[59].Piece.Moved, 56, 59);
                        board.Squares[59].Piece.Moved = true;
                        return;
                    }
                }
            }
            else if (piece.PieceColor == ChessPieceColor.Black && srcPosition == 4)
            {
                if (dstPosition == 6)
                {
                    //Ok we are casteling we need to move the Rook
                    if (board.Squares[7].Piece != null)
                    {
                        board.Squares[5].Piece = board.Squares[7].Piece;
                        board.Squares[7].Piece = null;
                        board.BlackCastled = true;
                        board.LastMove.MovingPieceSecondary = new PieceMoving(board.Squares[5].Piece.PieceColor, board.Squares[5].Piece.PieceType, board.Squares[5].Piece.Moved, 7, 5);
                        board.Squares[5].Piece.Moved = true;
                        return;
                    }
                }
                    //Castle Left
                else if (dstPosition == 2)
                {
                    //Ok we are casteling we need to move the Rook
                    if (board.Squares[0].Piece != null)
                    {
                        board.Squares[3].Piece = board.Squares[0].Piece;
                        board.Squares[0].Piece = null;
                        board.BlackCastled = true;
                        board.LastMove.MovingPieceSecondary = new PieceMoving(board.Squares[3].Piece.PieceColor, board.Squares[3].Piece.PieceType, board.Squares[3].Piece.Moved, 0, 3);
                        board.Squares[3].Piece.Moved = true;
                        return;
                    }
                }
            }

            return;
        }

        private Square GetKingSquare(ChessPieceColor chessPieceColor)
        {
            return Squares.First(x => x.Piece != null
                    && x.Piece.PieceType == ChessPieceType.King
                    && x.Piece.PieceColor == chessPieceColor);
        }

        #endregion

        #region InternalMethods

        //Fast Copy
        internal Board FastCopy()
        {
            Board clonedBoard = new Board(Squares);

            clonedBoard.WhoseMove = WhoseMove;
            clonedBoard.MoveCount = MoveCount;
            clonedBoard.FiftyMove = FiftyMove;
            clonedBoard.ZobristHash = ZobristHash;
            clonedBoard.BlackCastled = BlackCastled;
            clonedBoard.WhiteCastled = WhiteCastled;

            clonedBoard.WhiteCanCastle = WhiteCanCastle;
            clonedBoard.BlackCanCastle = BlackCanCastle;

            WhiteAttackBoard = new bool[64];
            BlackAttackBoard = new bool[64];

            return clonedBoard;
        }

        internal static MoveContent MovePiece(Board board, byte srcPosition, byte dstPosition, ChessPieceType promoteToPiece)
        {
            Piece piece = board.Squares[srcPosition].Piece;

            //Record my last move
            board.LastMove = new MoveContent();

            

            if (piece.PieceColor == ChessPieceColor.Black)
            {
                board.MoveCount++;
                //Add One to FiftyMoveCount to check for tie.
                board.FiftyMove++;
            }

            //En Passant
            if (board.EnPassantPosition > 0)
            {
                board.LastMove.EnPassantOccured = SetEnpassantMove(board, srcPosition, dstPosition, piece.PieceColor);
            }

            if (!board.LastMove.EnPassantOccured)
            {
                Square sqr = board.Squares[dstPosition];

                if (sqr.Piece != null)
                {
                    board.LastMove.TakenPiece = new PieceTaken(sqr.Piece.PieceColor, sqr.Piece.PieceType,
                                                               sqr.Piece.Moved, dstPosition);
                    board.FiftyMove = 0;
                }
                else
                {
                    board.LastMove.TakenPiece = new PieceTaken(ChessPieceColor.White, ChessPieceType.None, false,
                                                               dstPosition);
                    
                }
            }

            board.LastMove.MovingPiecePrimary = new PieceMoving(piece.PieceColor, piece.PieceType, piece.Moved, srcPosition, dstPosition);

            //Delete the piece in its source position
            board.Squares[srcPosition].Piece = null;
      
            //Add the piece to its new position
            piece.Moved = true;
            piece.Selected = false;
            board.Squares[dstPosition].Piece = piece;

            //Reset EnPassantPosition
            board.EnPassantPosition = 0;
          
            //Record En Passant if Pawn Moving
            if (piece.PieceType == ChessPieceType.Pawn)
            {
               board.FiftyMove = 0;
               RecordEnPassant(piece.PieceColor, piece.PieceType, board, srcPosition, dstPosition);
            }

            board.WhoseMove = board.WhoseMove == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;

            KingCastle(board, piece, srcPosition, dstPosition);

            //Promote Pawns 
            if (PromotePawns(board, piece, dstPosition, promoteToPiece))
            {
                board.LastMove.PawnPromotedTo = promoteToPiece;
            }
            else
            {
                board.LastMove.PawnPromotedTo = ChessPieceType.None;
            }

            return board.LastMove;
        }

        private static string GetColumnFromByte(byte column)
        {
            switch (column)
            {
                case 0:
                    return "a";
                case 1:
                    return "b";
                case 2:
                    return "c";
                case 3:
                    return "d";
                case 4:
                    return "e";
                case 5:
                    return "f";
                case 6:
                    return "g";
                case 7:
                    return "h";
                default:
                    return "a";
            }
        }

        public new string ToString()
        {
            return Fen(false, this);
        }

        internal static string Fen(bool boardOnly, Board board)
        {
            string output = String.Empty;
            byte blankSquares = 0;

            for (byte x = 0; x < 64; x++)
            {
                byte index = x;

                if (board.Squares[index].Piece != null)
                {
                    if (blankSquares > 0)
                    {
                        output += blankSquares.ToString();
                        blankSquares = 0;
                    }

                    if (board.Squares[index].Piece.PieceColor == ChessPieceColor.Black)
                    {
                        output += Piece.GetPieceTypeShort(board.Squares[index].Piece.PieceType).ToLower();
                    }
                    else
                    {
                        output += Piece.GetPieceTypeShort(board.Squares[index].Piece.PieceType);
                    }
                }
                else
                {
                    blankSquares++;
                }

                if (x % 8 == 7)
                {
                    if (blankSquares > 0)
                    {
                        output += blankSquares.ToString();
                        output += "/";
                        blankSquares = 0;
                    }
                    else
                    {
                        if (x > 0 && x != 63)
                        {
                            output += "/";
                        }
                    }
                }
            }

            if (board.WhoseMove == ChessPieceColor.White)
            {
                output += " w ";
            }
            else
            {
                output += " b ";
            }

            string spacer = "";

            if (board.WhiteCastled == false)
            {
                if (board.Squares[60].Piece != null)
                {
                    if (board.Squares[60].Piece.Moved == false)
                    {
                        if (board.Squares[63].Piece != null)
                        {
                            if (board.Squares[63].Piece.Moved == false)
                            {
                                output += "K";
                                spacer = " ";
                            }
                        }
                        if (board.Squares[56].Piece != null)
                        {
                            if (board.Squares[56].Piece.Moved == false)
                            {
                                output += "Q";
                                spacer = " ";
                            }
                        }
                    }
                }
            }

            if (board.BlackCastled == false)
            {
                if (board.Squares[4].Piece != null)
                {
                    if (board.Squares[4].Piece.Moved == false)
                    {
                        if (board.Squares[7].Piece != null)
                        {
                            if (board.Squares[7].Piece.Moved == false)
                            {
                                output += "k";
                                spacer = " ";
                            }
                        }
                        if (board.Squares[0].Piece != null)
                        {
                            if (board.Squares[0].Piece.Moved == false)
                            {
                                output += "q";
                                spacer = " ";
                            }
                        }
                    }
                }

                
            }

            if (output.EndsWith("/"))
            {
                output.TrimEnd('/');
            }


            if (board.EnPassantPosition != 0)
            {
                output += spacer + GetColumnFromByte((byte)(board.EnPassantPosition % 8)) + "" + (byte)(8 - (byte)(board.EnPassantPosition / 8)) + " ";
            }
            else
            {
                output += spacer + "- ";
            }

            if (!boardOnly)
            {
                output += board.FiftyMove + " ";
                output += board.MoveCount + 1;
            }
            return output.Trim();
        }

        #endregion

        #region FactoryMethods
        public static Board CreateBoardFromFen(string fen)
        {
            var result = new Board();

            byte index = 0;

            result.WhiteCastled = true;
            result.BlackCastled = true;
            result.WhiteCanCastle = false;
            result.BlackCanCastle = false;

            byte spacers = 0;

            result.WhoseMove = ChessPieceColor.White;

            if (fen.Contains("a3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 40;
            }
            else if (fen.Contains("b3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 41;
            }
            else if (fen.Contains("c3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 42;
            }
            else if (fen.Contains("d3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 43;
            }
            else if (fen.Contains("e3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 44;
            }
            else if (fen.Contains("f3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 45;
            }
            else if (fen.Contains("g3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 46;
            }
            else if (fen.Contains("h3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 47;
            }


            if (fen.Contains("a6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 16;
            }
            else if (fen.Contains("b6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 17;
            }
            else if (fen.Contains("c6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 18;
            }
            else if (fen.Contains("d6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 19;
            }
            else if (fen.Contains("e6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 20;
            }
            else if (fen.Contains("f6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 21;
            }
            else if (fen.Contains("g6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 22;
            }
            else if (fen.Contains("h6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 23;
            }

            if (fen.Contains(" w "))
            {
                result.WhoseMove = ChessPieceColor.White;
            }
            if (fen.Contains(" b "))
            {
                result.WhoseMove = ChessPieceColor.Black;
            }

            foreach (char c in fen)
            {

                if (index < 64 && spacers == 0)
                {
                    if (c == '1' && index < 63)
                    {
                        index++;
                    }
                    else if (c == '2' && index < 62)
                    {
                        index += 2;
                    }
                    else if (c == '3' && index < 61)
                    {
                        index += 3;
                    }
                    else if (c == '4' && index < 60)
                    {
                        index += 4;
                    }
                    else if (c == '5' && index < 59)
                    {
                        index += 5;
                    }
                    else if (c == '6' && index < 58)
                    {
                        index += 6;
                    }
                    else if (c == '7' && index < 57)
                    {
                        index += 7;
                    }
                    else if (c == '8' && index < 56)
                    {
                        index += 8;
                    }
                    else if (c == 'P')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'N')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'B')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'R')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'Q')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'K')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.King, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'p')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'n')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'b')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'r')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'q')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'k')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.King, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == '/')
                    {
                        continue;
                    }
                    else if (c == ' ')
                    {
                        spacers++;
                    }
                }
                else
                {
                    if (c == 'K')
                    {
                        if (result.Squares[60].Piece != null)
                        {
                            if (result.Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[60].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[63].Piece != null)
                        {
                            if (result.Squares[63].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[63].Piece.Moved = false;
                            }
                        }

                        result.WhiteCastled = false;
                        result.WhiteCanCastle = true;
                    }
                    else if (c == 'Q')
                    {
                        if (result.Squares[60].Piece != null)
                        {
                            if (result.Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[60].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[56].Piece != null)
                        {
                            if (result.Squares[56].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[56].Piece.Moved = false;
                            }
                        }
                        result.WhiteCanCastle = true;
                        result.WhiteCastled = false;
                    }
                    else if (c == 'k')
                    {
                        if (result.Squares[4].Piece != null)
                        {
                            if (result.Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[4].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[7].Piece != null)
                        {
                            if (result.Squares[7].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[7].Piece.Moved = false;
                            }
                        }

                        result.BlackCastled = false;
                        result.BlackCanCastle = true;
                    }
                    else if (c == 'q')
                    {
                        if (result.Squares[4].Piece != null)
                        {
                            if (result.Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[4].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[0].Piece != null)
                        {
                            if (result.Squares[0].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[0].Piece.Moved = false;
                            }
                        }

                        result.BlackCastled = false;
                        result.BlackCanCastle = true;
                    }
                    else if (c == ' ')
                    {
                        spacers++;
                    }
                    else if (c == '1' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 1);
                    }
                    else if (c == '2' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 2);
                    }
                    else if (c == '3' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 3);
                    }
                    else if (c == '4' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 4);
                    }
                    else if (c == '5' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 5);
                    }
                    else if (c == '6' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 6);
                    }
                    else if (c == '7' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 7);
                    }
                    else if (c == '8' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 8);
                    }
                    else if (c == '9' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 9);
                    }
                    else if (c == '0' && spacers == 4)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                    }
                    else if (c == '1' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 1);
                    }
                    else if (c == '2' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 2);
                    }
                    else if (c == '3' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 3);
                    }
                    else if (c == '4' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 4);
                    }
                    else if (c == '5' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 5);
                    }
                    else if (c == '6' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 6);
                    }
                    else if (c == '7' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 7);
                    }
                    else if (c == '8' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 8);
                    }
                    else if (c == '9' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 9);
                    }
                    else if (c == '0' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                    }
                }
            }
            return result;
        }

        public static Board CreateNewGameBoard()
        {
            var result = new Board();

            result.WhoseMove = ChessPieceColor.White;

            result.Squares[0].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);
            result.Squares[1].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
            result.Squares[2].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
            result.Squares[3].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.Black);
            result.Squares[4].Piece = new Piece(ChessPieceType.King, ChessPieceColor.Black);
            result.Squares[5].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
            result.Squares[6].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
            result.Squares[7].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);

            for (int i = 8; i < 16; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 8; i < 16; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 48; i < 56; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);

            result.Squares[56].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);
            result.Squares[57].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
            result.Squares[58].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
            result.Squares[59].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.White);
            result.Squares[60].Piece = new Piece(ChessPieceType.King, ChessPieceColor.White);
            result.Squares[61].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
            result.Squares[62].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
            result.Squares[63].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);

            //TODO: Remove line below
            PieceValidMoves.GenerateValidMoves(result);

            return result;
        }

        public static Board CreateEmptyBoard()
        {
            var result = new Board();

            for (byte i = 0; i < 64; i++)
            {
                result.Squares[i] = new Square();
            }

            result.LastMove = new MoveContent();

            result.BlackCanCastle = true;
            result.WhiteCanCastle = true;

            return result;
        }

        #endregion
    }
}