using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine.Enums
{
    public static class ChessPieceColor
    {
        public static readonly ChessColor White = new ChessColor { Colour = true };
        public static readonly ChessColor Black = new ChessColor { Colour = false };
    }

    public struct ChessColor
    {
        public bool Colour;

        public override bool Equals(object obj)
        {
            if (obj is ChessColor)
                return (Colour.Equals(((ChessColor)obj).Colour));
            return Colour.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Colour.GetHashCode();
        }

        public override string ToString()
        {
            return Colour.ToString();
        }

        public static bool operator ==(ChessColor a, ChessColor b)
        {
            return a.Colour == b.Colour;
        }

        public static bool operator !=(ChessColor a, ChessColor b)
        {
            return a.Colour != b.Colour;
        }

        public static ChessColor operator !(ChessColor a)
        {
            var result = a == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;
            return result;
        }
    }

}
