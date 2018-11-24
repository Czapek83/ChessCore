using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Rook : Piece
    {
        public Rook(ChessPieceColor color) : base(ChessPieceType.Rook, color)
        {

        }

        public override short PieceValue => 500;
        public override short PieceActionValue => 2;
        
        public override string GetPieceTypeShort()
        {
            return "R";
        }
    }
}
