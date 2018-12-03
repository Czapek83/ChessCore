using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine.Loggers
{
    public class ConsoleLogger : LoggerBase
    {
        public ConsoleLogger(LogLevel logLevel):base(logLevel)
        {

        }

        protected override void LogAllImpl(string message)
        {
            WriteMessage(message, ConsoleColor.Yellow);
        }

        protected override void LogDebugImpl(string message)
        {
            WriteMessage(message, ConsoleColor.White);
        }

        protected override void LogErrorImpl(string message)
        {
            WriteMessage(message, ConsoleColor.Red);
        }

        protected override void LogInfoImpl(string message)
        {
            WriteMessage(message, ConsoleColor.Green);
        }

        private void WriteMessage(string message, ConsoleColor consoleColor)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message, consoleColor);
            Console.ForegroundColor = previousColor;
        }
    }
}
