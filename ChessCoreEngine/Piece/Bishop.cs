using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Bishop : Piece
    {
        public Bishop(ChessPieceColor color) : base(ChessPieceType.Bishop, color)
        {
        }

        public override short PieceValue { get => 325; }
        public override short PieceActionValue { get => 3; }

        public override string GetPieceTypeShort()
        {
            return "B";
        }
    }
}
