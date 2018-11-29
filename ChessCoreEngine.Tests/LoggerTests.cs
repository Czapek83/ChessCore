using AutoFixture;
using ChessEngine.Engine;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        private Fixture fixture = new Fixture();


        [TestCase(LogLevel.Debug, 1, 1, 1)]
        [TestCase(LogLevel.Info, 0, 1, 1)]
        [TestCase(LogLevel.Error, 0, 0, 1)]
        [TestCase(LogLevel.NoLog, 0, 0, 0)]
        public void LoggerTest(LogLevel logLevel, int logDebugTimes, int logInfoTimes,
            int logErrorTimes)
        {
            var mock = new Mock<LoggerBase>(logLevel);
            mock.Protected().Setup("LogDebugImpl", ItExpr.IsAny<string>());
            mock.Protected().Setup("LogInfoImpl", ItExpr.IsAny<string>());
            mock.Protected().Setup("LogErrorImpl", ItExpr.IsAny<string>());
            var systemUnderTests = mock.Object;

            systemUnderTests.LogDebug(fixture.Create<string>());
            systemUnderTests.LogInfo(fixture.Create<string>());
            systemUnderTests.LogError(fixture.Create<string>());

            mock.Protected().Verify("LogDebugImpl", Times.Exactly(logDebugTimes), ItExpr.IsAny<string>());
            mock.Protected().Verify("LogInfoImpl", Times.Exactly(logInfoTimes), ItExpr.IsAny<string>());
            mock.Protected().Verify("LogErrorImpl", Times.Exactly(logErrorTimes), ItExpr.IsAny<string>());

        }

    }
}
