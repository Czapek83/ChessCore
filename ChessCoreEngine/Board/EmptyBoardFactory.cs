using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class EmptyBoardFactory : IBoardFactory
    {
        public Board CreateBoard()
        {
            var result = new Board();

            for (byte i = 0; i < 64; i++)
            {
                result.Squares[i] = new Square();
            }

            result.LastMove = new MoveContent();

            result.BlackCanCastle = true;
            result.WhiteCanCastle = true;

            return result;
        }
    }
}
