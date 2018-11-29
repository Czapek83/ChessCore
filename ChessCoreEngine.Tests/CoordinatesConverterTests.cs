using ChessEngine.Engine;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class CoordinatesConverterTests
    {
        private CoordinatesConverter _systemUnderTests;
        [SetUp]
        public void SetUp()
        {
            _systemUnderTests = new CoordinatesConverter();
        }

        [Test]
        public void GetPositionByChessColorTests()
        {
            for (byte i = 0; i < 64; i++)
            {
                _systemUnderTests.GetPositionByChessColor(i, ChessPieceColor.Black).Should().Be((byte)(63 - i));
                _systemUnderTests.GetPositionByChessColor(i, ChessPieceColor.White).Should().Be(i);
            }
        }
    }
}
