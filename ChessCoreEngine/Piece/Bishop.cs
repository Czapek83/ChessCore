using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
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

        public Bishop(ChessPieceColor color) : base(ChessPieceType.Bishop, color)
        {
        }

        public override short PieceValue { get => 325; }
        public override short PieceActionValue { get => 3; }

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, byte index, short[] whitePawnTable, short[] blackPawnTable)
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
    }
}
