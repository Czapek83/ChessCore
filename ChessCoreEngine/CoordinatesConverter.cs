using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class CoordinatesConverter
    {
        private const byte _boardSize = 63;

        public virtual byte GetPositionByChessColor(byte originPosition, ChessColor chessPieceColor)
        {
            if (chessPieceColor == ChessPieceColor.White)
                return (byte)(_boardSize - originPosition);
            else return originPosition;
        }
    }
}
