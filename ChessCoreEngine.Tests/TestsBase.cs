using ChessEngine.Engine.Loggers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Tests
{
    public class TestsBase
    {
        protected LoggerBase _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new Mock<LoggerBase>(LogLevel.NoLog).Object;
        }
    }
}
