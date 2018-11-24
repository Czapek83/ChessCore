using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class King : Piece
    {
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
    }
}
