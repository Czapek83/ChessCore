using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class PawnCount
    {
        public Dictionary<ChessColor, short[]> Value;

        public PawnCount()
        {
            Value = new Dictionary<ChessColor, short[]>();
            Value.Add(ChessPieceColor.White, new short[8]);
            Value.Add(ChessPieceColor.Black, new short[8]);
        }

        public short[] this[ChessColor index]
        {
            get
            {
                return Value[index];
            }
        }
    }
}
