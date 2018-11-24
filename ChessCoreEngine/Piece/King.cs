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
    }
}
