using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Pawn : Piece
    {
        public Pawn(ChessPieceColor color): base(ChessPieceType.Pawn, color)
        {
            LastValidMoveCount = 2;

        }
        public override short PieceValue { get => 100; }
        public override short PieceActionValue => 6;
        public override string GetPieceTypeShort()
        {
            return "P";
        }
    }
}
