using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public enum LogLevel
    {
        All,
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

        public void LogAll(string message)
        {
            if (_logLevel == LogLevel.All)
                LogAllImpl(message);
        }

        public void LogDebug(string message)
        {
            if (_logLevel == LogLevel.Debug || _logLevel == LogLevel.All)
                LogDebugImpl(message);
        }

        public void LogInfo(string message)
        {
            if (_logLevel == LogLevel.Debug || _logLevel == LogLevel.Info || _logLevel == LogLevel.All)
                LogInfoImpl(message);
        }

        public void LogError(string message)
        {
            if (_logLevel != LogLevel.NoLog)
                LogErrorImpl(message);
        }

        protected abstract void LogAllImpl(string message);
        protected abstract void LogDebugImpl(string message);
        protected abstract void LogInfoImpl(string message);
        protected abstract void LogErrorImpl(string message);
    }
}
