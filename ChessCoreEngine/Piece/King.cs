using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class King : Piece
    {
        public static readonly short[] KingTable = new short[]
        {
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -20, -30, -30, -40, -40, -30, -30, -20,
          -10, -20, -20, -20, -20, -20, -20, -10,
           20,  20,   0,   0,   0,   0,  20,  20,
           20,  30,  10,   0,   0,  10,  30,  20
        };

        public static readonly short[] KingTableEndGame = new short[]
        {
            -50,-40,-30,-20,-20,-30,-40,-50,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-30,  0,  0,  0,  0,-30,-30,
            -50,-30,-30,-30,-30,-30,-30,-50
        };


        public King(ChessPieceColor color) : base(ChessPieceType.King, color)
        {

        }

        public override bool IsKing()
        {
            return true;
        }
        public override short PieceValue => 32767;
        public override short PieceActionValue => 1;

        public override string GetPieceTypeShort()
        {
            return "K";
        }

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, byte index, short[] whitePawnTable, short[] blackPawnTable)
        {
            var score = 0;

            if (ValidMoves != null)
            {
                if (ValidMoves.Count < 2)
                {
                    score -= 5;
                }
            }

            if (endGamePhase)
            {
                score += KingTableEndGame[index];
            }
            else
            {
                score += KingTable[index];
            }

            return score;
        }

        public override void GenerateMoves(byte piecePosition, Board board)
        {
            if (PieceColor == ChessPieceColor.White)
            {
                if (Moved)
                {
                    board.SetCantCastle(ChessPieceColor.White);
                }
            }
            else
            {
                if (Moved)
                {
                    board.SetCantCastle(ChessPieceColor.Black);
                }
            }

            GenerateValidMovesKing(piecePosition, board);
            if (PieceColor == ChessPieceColor.White)
            {
                if (!board.WhiteCastled && board.WhiteCanCastle && !board.WhiteIsChecked)
                {
                    GenerateValidMovesKingCastle(board);
                }
            }
            else
            {
                if (!board.BlackCastled && board.BlackCanCastle && !board.BlackIsChecked)
                {
                    GenerateValidMovesKingCastle(board);
                }
            }
                
        }

        internal override GenerateValidMovesPriority GetGenerateValidMovesPriority(ChessPieceColor colorThatIsOnMove)
        {
            if (colorThatIsOnMove == PieceColor)
                return GenerateValidMovesPriority.Lowest;
            else
                return GenerateValidMovesPriority.Low;
        }

        private void GenerateValidMovesKing(byte srcPosition, Board board)
        {
            for (byte i = 0; i < MoveArrays.KingMoves[srcPosition].Moves.Count; i++)
            {
                byte dstPos = MoveArrays.KingMoves[srcPosition].Moves[i];

                if (PieceColor == ChessPieceColor.White)
                {
                    //I can't move where I am being attacked
                    if (board.BlackAttackBoard[dstPos])
                    {
                        board.WhiteAttackBoard[dstPos] = true;
                        continue;
                    }
                }
                else
                {
                    if (board.WhiteAttackBoard[dstPos])
                    {
                        board.BlackAttackBoard[dstPos] = true;
                        continue;
                    }
                }

                AnalyzeMove(dstPos, board);
            }
        }

        private void GenerateValidMovesKingCastle(Board board)
        {
            //This code will add the castleling move to the pieces available moves
            if (PieceColor == ChessPieceColor.White)
            {
                if (board.Squares[63].Piece != null)
                {
                    //Check if the Right Rook is still in the correct position
                    if (board.Squares[63].Piece.PieceType == ChessPieceType.Rook)
                    {
                        if (board.Squares[63].Piece.PieceColor == PieceColor)
                        {
                            //Move one column to right see if its empty
                            if (board.Squares[62].Piece == null)
                            {
                                if (board.Squares[61].Piece == null)
                                {
                                    if (board.BlackAttackBoard[61] == false &&
                                        board.BlackAttackBoard[62] == false)
                                    {
                                        //Ok looks like move is valid lets add it
                                        ValidMoves.Push(62);
                                        board.WhiteAttackBoard[62] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (board.Squares[56].Piece != null)
                {
                    //Check if the Left Rook is still in the correct position
                    if (board.Squares[56].Piece.PieceType == ChessPieceType.Rook)
                    {
                        if (board.Squares[56].Piece.PieceColor == PieceColor)
                        {
                            //Move one column to right see if its empty
                            if (board.Squares[57].Piece == null)
                            {
                                if (board.Squares[58].Piece == null)
                                {
                                    if (board.Squares[59].Piece == null)
                                    {
                                        if (board.BlackAttackBoard[58] == false &&
                                            board.BlackAttackBoard[59] == false)
                                        {
                                            //Ok looks like move is valid lets add it
                                            ValidMoves.Push(58);
                                            board.WhiteAttackBoard[58] = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (PieceColor == ChessPieceColor.Black)
            {
                //There are two ways to castle, scenario 1:
                if (board.Squares[7].Piece != null)
                {
                    //Check if the Right Rook is still in the correct position
                    if (board.Squares[7].Piece.PieceType == ChessPieceType.Rook
                        && !board.Squares[7].Piece.Moved)
                    {
                        if (board.Squares[7].Piece.PieceColor == PieceColor)
                        {
                            //Move one column to right see if its empty

                            if (board.Squares[6].Piece == null)
                            {
                                if (board.Squares[5].Piece == null)
                                {
                                    if (board.WhiteAttackBoard[5] == false && board.WhiteAttackBoard[6] == false)
                                    {
                                        //Ok looks like move is valid lets add it
                                        ValidMoves.Push(6);
                                        board.BlackAttackBoard[6] = true;
                                    }
                                }
                            }
                        }
                    }
                }
                //There are two ways to castle, scenario 2:
                if (board.Squares[0].Piece != null)
                {
                    //Check if the Left Rook is still in the correct position
                    if (board.Squares[0].Piece.PieceType == ChessPieceType.Rook &&
                        !board.Squares[0].Piece.Moved)
                    {
                        if (board.Squares[0].Piece.PieceColor ==
                            PieceColor)
                        {
                            //Move one column to right see if its empty
                            if (board.Squares[1].Piece == null)
                            {
                                if (board.Squares[2].Piece == null)
                                {
                                    if (board.Squares[3].Piece == null)
                                    {
                                        if (board.WhiteAttackBoard[2] == false &&
                                            board.WhiteAttackBoard[3] == false)
                                        {
                                            //Ok looks like move is valid lets add it
                                            ValidMoves.Push(2);
                                            board.BlackAttackBoard[2] = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
