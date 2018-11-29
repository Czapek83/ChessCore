using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Pawn : Piece
    {
        public static readonly short[] PawnTable = new short[]
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            50, 50, 50, 50, 50, 50, 50, 50,
            20, 20, 30, 40, 40, 30, 20, 20,
             5,  5, 10, 30, 30, 10,  5,  5,
             0,  0,  0, 25, 25,  0,  0,  0,
             5, -5,-10,  0,  0,-10, -5,  5,
             5, 10, 10,-30,-30, 10, 10,  5,
             0,  0,  0,  0,  0,  0,  0,  0
        };

        public Pawn(ChessColor color, ICoordinatesConverter coordinatesConverter) : base(ChessPieceType.Pawn, color, coordinatesConverter)
        {
            LastValidMoveCount = 2;
        }
        public override short PieceValue { get => 100; }
        public override short PieceActionValue => 6;
        public override string GetPieceTypeShort()
        {
            return "P";
        }

        public override int EvaluatePieceSpecificScore(byte position,
            bool endGamePhase, byte index, PawnCount pawnCount)
        {
            int score = 0;

            if (position % 8 == 0 || position % 8 == 7)
            {
                //Rook Pawns are worth 15% less because they can only attack one way
                score -= 15;
            }

            //Calculate Position Values
            score += PawnTable[index];

            if (PieceColor == ChessPieceColor.White)
            {
                if (pawnCount[ChessPieceColor.White][position % 8] > 0)
                {
                    //Doubled Pawn
                    score -= 15;
                }

                if (position >= 8 && position <= 15)
                {
                    if (AttackedValue == 0)
                    {
                        pawnCount[ChessPieceColor.White][position % 8] += 50;

                        if (DefendedValue != 0)
                            pawnCount[ChessPieceColor.White][position % 8] += 50;
                    }
                }
                else if (position >= 16 && position <= 23)
                {
                    if (AttackedValue == 0)
                    {
                        pawnCount[ChessPieceColor.White][position % 8] += 25;


                        if (DefendedValue != 0)
                            pawnCount[ChessPieceColor.White][position % 8] += 25;
                    }
                }

                pawnCount[ChessPieceColor.White][position % 8] += 10;
            }
            else
            {
                if (pawnCount[ChessPieceColor.Black][position % 8] > 0)
                {
                    //Doubled Pawn
                    score -= 15;
                }

                if (position >= 48 && position <= 55)
                {
                    if (AttackedValue == 0)
                    {
                        pawnCount[ChessPieceColor.Black][position % 8] += 50;

                        if (DefendedValue != 0)
                            pawnCount[ChessPieceColor.Black][position % 8] += 50;
                    }
                }
                //Pawns in 6th Row that are not attacked are worth more points.
                else if (position >= 40 && position <= 47)
                {
                    if (AttackedValue == 0)
                    {
                        pawnCount[ChessPieceColor.Black][position % 8] += 25;

                        if (DefendedValue != 0)
                            pawnCount[ChessPieceColor.Black][position % 8] += 25;
                    }
                }

                pawnCount[ChessPieceColor.Black][position % 8] += 10;
            }
                return score;
        }

        public override void GenerateMoves(byte piecePosition, Board board)
        {
            var moves = PieceColor == ChessPieceColor.White ?
                MoveArrays.WhitePawnMoves[piecePosition].Moves : MoveArrays.BlackPawnMoves[piecePosition].Moves;
            for (byte i = 0; i < moves.Count; i++)
            {
                byte dstPos = moves[i];

                //Diagonal
                if (dstPos % 8 != piecePosition % 8)
                {
                    //If there is a piece there I can potentialy kill
                    AnalyzeMovePawn(dstPos, board);

                    board.AttackBoard[PieceColor][dstPos] = true;
                }
                // if there is something if front pawns can't move there
                else if (board.GetPiece(dstPos) != null)
                {
                    return;
                }
                //if there is nothing in front of 
                else
                {
                    ValidMoves.Push(dstPos);
                }
            }
        }

        private void AnalyzeMovePawn(byte dstPos, Board board)
        {
            //Because Pawns only kill diagonaly we handle the En Passant scenario specialy
            if (board.EnPassantPosition > 0)
            {
                if (PieceColor != board.EnPassantColor)
                {
                    if (board.EnPassantPosition == dstPos)
                    {
                        //We have an En Passant Possible
                        ValidMoves.Push(dstPos);
                        board.AttackBoard[PieceColor][dstPos] = true;
                    }
                }
            }

            Piece pcAttacked = board.GetPiece(dstPos);

            //If there no piece there I can potentialy kill
            if (pcAttacked == null)
                return;

            board.AttackBoard[PieceColor][dstPos] = true;

            //if that piece is the same color
            if (pcAttacked.PieceColor == PieceColor)
            {
                pcAttacked.DefendedValue += PieceActionValue;
                return;
            }

            pcAttacked.AttackedValue += PieceActionValue;

            //If this is a king set it in check                   
            if (pcAttacked.PieceType == ChessPieceType.King)
            {
                board.SetCheckedSide(!PieceColor);
            }
            else
            {
                //Add this as a valid move
                ValidMoves.Push(dstPos);
            }

            return;
        }
    }
}
