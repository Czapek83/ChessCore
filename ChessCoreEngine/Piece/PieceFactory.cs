using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine.Pieces
{
    public static class PieceFactory
    {
        public static Piece CreatePieceByTypeAndColor(ChessPieceType chesspieceType,
            ChessColor chessPieceColor)
        {
            var converter = new CoordinatesConverter();
            switch (chesspieceType)
            {
                case ChessPieceType.Bishop:
                    return new Bishop(chessPieceColor, converter);
                case ChessPieceType.Knight:
                    return new Knight(chessPieceColor, converter);
                case ChessPieceType.Rook:
                    return new Rook(chessPieceColor, converter);
                case ChessPieceType.Queen:
                    return new Queen(chessPieceColor, converter);
                case ChessPieceType.King:
                    return new King(chessPieceColor, converter);
                case ChessPieceType.Pawn:
                    return new Pawn(chessPieceColor, converter);
                default: throw new ArgumentException($"Invalid chessPieceType {chesspieceType}");
            }
        }
    }
}
