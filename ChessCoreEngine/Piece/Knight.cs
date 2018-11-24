using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Knight : Piece
    {
        public static readonly short[] KnightTable = new short[]
        {
            -50,-40,-30,-30,-30,-30,-40,-50,
            -40,-20,  0,  0,  0,  0,-20,-40,
            -30,  0, 10, 15, 15, 10,  0,-30,
            -30,  5, 15, 20, 20, 15,  5,-30,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -30,  5, 10, 15, 15, 10,  5,-30,
            -40,-20,  0,  5,  5,  0,-20,-40,
            -50,-30,-20,-30,-30,-20,-30,-50,
        };

        public Knight(ChessPieceColor color) : base(ChessPieceType.Knight, color)
        {
            LastValidMoveCount = 2;
        }

        public override short PieceValue => 320;
        public override short PieceActionValue => 3;

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, 
            byte index, short[] whitePawnTable, short[] blackPawnTable)
        {
            var score = 0;

            score += KnightTable[index];

            //In the end game remove a few points for Knights since they are worth less
            if (endGamePhase)
            {
                score -= 10;
            }

            return score;
        }

        public override string GetPieceTypeShort()
        {
            return "N";
        }
    }
}
