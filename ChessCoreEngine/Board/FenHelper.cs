using ChessEngine.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Board
{
    public class FenHelper
    {
        public (ChessColor enPassantColor, byte enPassantPosition)? GetEnPassantDataFromFen(string fen)
        {
            if (fen.Contains("a3"))
            {
                return (ChessPieceColor.White, 40);
            }
            else if (fen.Contains("b3"))
            {
                return (ChessPieceColor.White, 41);
            }
            else if (fen.Contains("c3"))
            {
                return (ChessPieceColor.White, 42);
            }
            else if (fen.Contains("d3"))
            {
                return (ChessPieceColor.White, 43);
            }
            else if (fen.Contains("e3"))
            {
                return (ChessPieceColor.White, 44);
            }
            else if (fen.Contains("f3"))
            {
                return (ChessPieceColor.White, 45);
            }
            else if (fen.Contains("g3"))
            {
                return (ChessPieceColor.White, 46);
            }
            else if (fen.Contains("h3"))
            {
                return (ChessPieceColor.White, 47);
            }


            if (fen.Contains("a6"))
            {
                return (ChessPieceColor.Black, 16);
            }
            else if (fen.Contains("b6"))
            {
                return (ChessPieceColor.Black, 17);
            }
            else if (fen.Contains("c6"))
            {
                return (ChessPieceColor.Black, 18);
            }
            else if (fen.Contains("d6"))
            {
                return (ChessPieceColor.Black, 19);
            }
            else if (fen.Contains("e6"))
            {
                return (ChessPieceColor.Black, 20);
            }
            else if (fen.Contains("f6"))
            {
                return (ChessPieceColor.Black, 21);
            }
            else if (fen.Contains("g6"))
            {
                return (ChessPieceColor.Black, 22);
            }
            else if (fen.Contains("h6"))
            {
                return (ChessPieceColor.Black, 23);
            }

            return null;
        }
    }
}
