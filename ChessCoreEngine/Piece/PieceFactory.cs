using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public static class PieceFactory
    {
        public static Piece CreatePieceByTypeAndColor(ChessPieceType chesspieceType,
            ChessPieceColor chessPieceColor)
        {
            switch (chesspieceType)
            {
                case ChessPieceType.Bishop:
                    return new Bishop(chessPieceColor);
                case ChessPieceType.Knight:
                    return new Knight(chessPieceColor);
                case ChessPieceType.Rook:
                    return new Rook(chessPieceColor);
                case ChessPieceType.Queen:
                    return new Queen(chessPieceColor);
                case ChessPieceType.King:
                    return new King(chessPieceColor);
                case ChessPieceType.Pawn:
                    return new Pawn(chessPieceColor);
                default: throw new ArgumentException($"Invalid chessPieceType {chesspieceType}");
            }
        }
    }
}
