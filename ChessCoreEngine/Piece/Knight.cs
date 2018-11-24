using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Knight : Piece
    {
        public Knight(ChessPieceColor color) : base(ChessPieceType.Knight, color)
        {
            LastValidMoveCount = 2;
        }

        public override short PieceValue => 320;
        public override short PieceActionValue => 3;
        
        public override string GetPieceTypeShort()
        {
            return "N";
        }
    }
}
