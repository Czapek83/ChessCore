using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public enum LogLevel
    {
        Debug,
        Info,
        Error,
        NoLog
    }

    public abstract class LoggerBase
    {
        protected readonly LogLevel _logLevel;

        protected LoggerBase(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void LogDebug(string message)
        {
            if (_logLevel == LogLevel.Debug)
                LogDebugImpl(message);
        }

        public void LogInfo(string message)
        {
            if (_logLevel == LogLevel.Debug || _logLevel == LogLevel.Info)
                LogInfoImpl(message);
        }

        public void LogError(string message)
        {
            if (_logLevel != LogLevel.NoLog)
                LogErrorImpl(message);
        }

        protected abstract void LogDebugImpl(string message);
        protected abstract void LogInfoImpl(string message);
        protected abstract void LogErrorImpl(string message);
    }
}
