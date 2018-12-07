using ChessCoreEngine.Board;
using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Loggers;
using ChessEngine.Engine.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public abstract class BoardFactory : Board
    {
        public BoardFactory(LoggerBase logger):base(logger)
        {
            _logger.LogDebug($"BoardFactory(LoggerBase logger)");
        }

        public abstract Board CreateBoard();
    }
}
