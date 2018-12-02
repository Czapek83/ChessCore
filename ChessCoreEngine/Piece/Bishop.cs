using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine.Pieces
{
    public class Bishop : Piece
    {
        public static readonly short[] BishopTable = new short[]
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5, 10, 10,  5,  0,-10,
            -10,  5,  5, 10, 10,  5,  5,-10,
            -10,  0, 10, 10, 10, 10,  0,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  5,  0,  0,  0,  0,  5,-10,
            -20,-10,-40,-10,-10,-40,-10,-20,
        };

        public Bishop(ChessColor color, ICoordinatesConverter coordinatesConverter) : base(ChessPieceType.Bishop, color, coordinatesConverter)
        {
        }

        public override short PieceValue { get => 325; }
        public override short PieceActionValue { get => 3; }

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, byte index, PawnCountDictionary _)
        {
            var score = 0;

            //In the end game Bishops are worth more
            if (endGamePhase)
            {
                score += 10;
            }

            score += BishopTable[index];

            return score;
        }

        public override string GetPieceTypeShort()
        {
            return "B";
        }

        public override void GenerateMoves(byte piecePosition, Board board)
        {
            for (byte i = 0; i < MoveArrays.BishopMoves1[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.BishopMoves1[piecePosition].Moves[i],
                                board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.BishopMoves2[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.BishopMoves2[piecePosition].Moves[i],
                                board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.BishopMoves3[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.BishopMoves3[piecePosition].Moves[i],
                                board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.BishopMoves4[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.BishopMoves4[piecePosition].Moves[i],
                                board) ==
                    false)
                {
                    break;
                }
            }
        }
    }
}
