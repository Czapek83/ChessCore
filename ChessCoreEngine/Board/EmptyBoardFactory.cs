using ChessEngine.Engine.Loggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class EmptyBoardFactory : BoardFactory
    {
        public EmptyBoardFactory(LoggerBase logger) : base(logger)
        {

        }

        public override Board CreateBoard()
        {
            _logger.LogInfo($"BoardFactory.CreateEmptyBoard() start");
            for (byte i = 0; i < 64; i++)
            {
                Squares[i] = new Square();
            }

            LastMove = new MoveContent();

            BlackCanCastle = true;
            WhiteCanCastle = true;
            _logger.LogDebug($"BoardFactory.CreateEmptyBoard() end");
            return this;
        }
    }
}
