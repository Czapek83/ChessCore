using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public interface IBoardFactory
    {
        Board CreateBoard();
    }
}
