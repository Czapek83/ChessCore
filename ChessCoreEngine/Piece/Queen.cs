using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Queen : Piece
    {
        public Queen(ChessPieceColor color) : base(ChessPieceType.Queen, color)
        {

        }

        public override short PieceValue => 975;
        public override short PieceActionValue => 1;
        
        public override string GetPieceTypeShort()
        {
            return "Q";
        }
    }
}
