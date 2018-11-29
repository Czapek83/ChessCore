using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class DictionaryBase<T>
    {
        public Dictionary<ChessColor, T[]> Value;

        public DictionaryBase(int size)
        {
            Value = new Dictionary<ChessColor, T[]>();
            Value.Add(ChessPieceColor.White, new T[size]);
            Value.Add(ChessPieceColor.Black, new T[size]);
        }

        public T[] this[ChessColor index]
        {
            get
            {
                return Value[index];
            }
        }
    }
}
