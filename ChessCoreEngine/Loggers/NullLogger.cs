using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine.Loggers
{
    public class NullLogger : LoggerBase
    {
        public NullLogger():base(LogLevel.NoLog)
        {

        }

        protected override void LogAllImpl(string message)
        {
            throw new NotImplementedException();
        }

        protected override void LogDebugImpl(string message)
        {
            throw new NotImplementedException();
        }

        protected override void LogErrorImpl(string message)
        {
            throw new NotImplementedException();
        }

        protected override void LogInfoImpl(string message)
        {
            throw new NotImplementedException();
        }
    }
}
