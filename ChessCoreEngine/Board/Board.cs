using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ChessCoreEngine.Tests")]

namespace ChessEngine.Engine
{
    public class Board
    {
        internal Square[] Squares { get; private set; }

        internal AttackBoardDictionary AttackBoard { get; private set; }

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
                && x.PieceType != ChessPieceType.Knight && !x.IsKing()))
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

        internal byte GetKingPosition(ChessColor chessColor)
        {
            return (byte)new List<Square>(Squares).IndexOf(GetKingSquare(chessColor));
        }
        
        public byte FiftyMove { get; protected set; }

        public MoveContent LastMove { get; protected set; }

        //Who initated En Passant
        public ChessColor EnPassantColor { get; protected set; }
        //Positions liable to En Passant
        public byte EnPassantPosition { get; protected set; }

        public int Score;

        public bool IsChecked(ChessColor checkedColor)
        {
            return AttackBoard[!checkedColor][GetKingPosition(checkedColor)];
        }

        public bool BlackMate;

        public bool WhiteMate;

        public byte RepeatedMove;

        public bool BlackCastled { get; protected set; }
        public bool WhiteCastled { get; protected set; }

        public bool BlackCanCastle { get; protected set; }
        public bool WhiteCanCastle { get; protected set; }

        internal ChessColor WhoseMove;
        
        internal int MoveCount;

        #region Constructors

        protected Board()
        {
            Squares = new Square[64];

            for (byte i = 0; i < 64; i++)
            {
                Squares[i] = new Square();
            }

            LastMove = new MoveContent();

            BlackCanCastle = true;
            WhiteCanCastle = true;

            AttackBoard = new AttackBoardDictionary();
        }

        private Board(Square[] squares)
        {
            Squares = new Square[64];

            for (byte x = 0; x < 64; x++)
            {
                if (squares[x].Piece != null)
                {
                    var pieceFrom = squares[x].Piece;
                    Squares[x].Piece = PieceFactory.CreatePieceByTypeAndColor(pieceFrom.PieceType, pieceFrom.PieceColor);
                }
            }

            AttackBoard = new AttackBoardDictionary();

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

            AttackBoard = new AttackBoardDictionary();

            for (byte x = 0; x < 64; x++)
            {
                AttackBoard[ChessPieceColor.White][x] = board.AttackBoard[ChessPieceColor.White][x];
                AttackBoard[ChessPieceColor.Black][x] = board.AttackBoard[ChessPieceColor.Black][x];
            }

            FiftyMove = board.FiftyMove;
            RepeatedMove = board.RepeatedMove;
           
            WhiteCastled = board.WhiteCastled;
            BlackCastled = board.BlackCastled;

            WhiteCanCastle = board.WhiteCanCastle;
            BlackCanCastle = board.BlackCanCastle;

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
                    board.Squares[dstPosition].Piece = PieceFactory.CreatePieceByTypeAndColor(promoteToPiece, ChessPieceColor.White);
                    return true;
                }
                if (dstPosition > 55)
                {
                    board.Squares[dstPosition].Piece= PieceFactory.CreatePieceByTypeAndColor(promoteToPiece, ChessPieceColor.Black);
                    return true;
                }
            }

            return false;
        }

        private static void RecordEnPassant(ChessColor pcColor, ChessPieceType pcType, Board board, byte srcPosition, byte dstPosition)
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

        private static bool SetEnpassantMove(Board board, byte srcPosition, byte dstPosition, ChessColor pcColor)
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

        private Square GetKingSquare(ChessColor chessPieceColor)
        {
            return Squares.First(x => x.Piece != null
                    && x.Piece.PieceType == ChessPieceType.King
                    && x.Piece.PieceColor == chessPieceColor);
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

            AttackBoard = new AttackBoardDictionary();

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
                        output += board.Squares[index].Piece.GetPieceTypeShort().ToLower();
                    }
                    else
                    {
                        output += board.Squares[index].Piece.GetPieceTypeShort();
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

        internal Piece GetPiece(byte position)
        {
            return this.Squares[position].Piece;
        }

        internal EvaluationParameters GetEvaluationParameters()
        {
            return new EvaluationParameters
            {
                BlackCanCastle = this.BlackCanCastle,
                BlackCastled = this.BlackCastled,
                BlackMate = this.BlackMate,
                EndGamePhase = this.EndGamePhase,
                FiftyMove = this.FiftyMove,
                InsufficientMaterial = this.InsufficientMaterial,
                IsDraw = this.IsDraw,
                RepeatedMove = this.RepeatedMove,
                WhiteCanCastle = this.WhiteCanCastle,
                WhiteCastled = this.WhiteCastled,
                WhiteMate = this.WhiteMate,
                WhoseMove = this.WhoseMove
            };
        }

        internal void SetCantCastle(ChessColor whichColorCantCastle)
        {
            if (whichColorCantCastle == ChessPieceColor.White)
                WhiteCanCastle = false;
            else if (whichColorCantCastle == ChessPieceColor.Black)
                BlackCanCastle = false;
        }

        internal void GenerateValidMoves()
        {
            //Generate Moves
            var pieces = Squares
                .Select((sqr, i) => new { i, sqr })
                .Where(x => x.sqr.Piece != null)
                .OrderByDescending(y => y.sqr.Piece.GetGenerateValidMovesPriority(WhoseMove))
                .Select(z => new { Index = (byte)z.i, z.sqr.Piece });

            foreach (var item in pieces)
            {
                item.Piece.ValidMoves = new Stack<byte>(item.Piece.LastValidMoveCount);
                item.Piece.GenerateMoves(item.Index, this);
            }
        }


        #endregion
    }
}