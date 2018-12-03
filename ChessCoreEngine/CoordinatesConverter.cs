using ChessEngine.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{

    public interface ICoordinatesConverter
    {
        byte GetPositionByChessColor(byte originPosition, ChessColor chessPieceColor);
    }

    public class CoordinatesConverter : ICoordinatesConverter
    {
        private const byte _boardSize = 63;

        public virtual byte GetPositionByChessColor(byte originPosition, ChessColor chessPieceColor)
        {
            if (chessPieceColor == ChessPieceColor.Black)
                return (byte)(_boardSize - originPosition);
            else return originPosition;
        }
    }
}
