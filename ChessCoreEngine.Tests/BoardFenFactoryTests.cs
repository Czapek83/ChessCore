using ChessCoreEngine.Board;
using ChessEngine.Engine;
using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Loggers;
using ChessEngine.Tests;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class BoardFenFactoryTests : TestsBase
    {
        [Test]
        public void TestBoardExampleFenConstructor()
        {
            var boardFromFen = new FenBoardFactory(new FenHelper(), "5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 41", _logger).CreateBoard();

            boardFromFen.Squares[5].Piece.PieceColor.Should().Be(ChessPieceColor.Black);
            boardFromFen.Squares[5].Piece.PieceType.Should().Be(ChessPieceType.King);

            boardFromFen.Squares[8].Piece.Should().Be(null);

            boardFromFen.Squares[62].Piece.PieceColor.Should().Be(ChessPieceColor.White);
            boardFromFen.Squares[62].Piece.PieceType.Should().Be(ChessPieceType.King);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", 3)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24", 8)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 1", 0)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", 0)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", 4)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", 0)]
        public void TestFiftyMoveFromFen(string fen, byte fiftyMoves)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.FiftyMove.Should().Be(fiftyMoves);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", 33)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24", 24)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 1", 1)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", 1)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", 13)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", 13)]
        public void TestMoveNumberFromFen(string fen, byte moveNumbers)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.MoveCount.Should().Be(moveNumbers);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", false)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24", false)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 1", false)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", false)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", true)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", true)]
        public void TestWhoseMoveFromFen(string fen, bool whoseMoveBool)
        {
            var whoseMove = whoseMoveBool ? ChessPieceColor.White : ChessPieceColor.Black;

            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.WhoseMove.Should().Be(whoseMove);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", false)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b kK - 8 24", true)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 1", false)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", false)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", false)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", false)]
        public void TestCanWhiteCastleFromFen(string fen, bool whiteCanCastle)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.WhiteCanCastle.Should().Be(whiteCanCastle);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", false)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24", false)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b q - 0 1", true)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", false)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", false)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", true)]
        public void TestCanBlackCastleFromFen(string fen, bool blackCanCastle)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.BlackCanCastle.Should().Be(blackCanCastle);
        }

        //TODO: Identify somehow from fen that white or black really castled.
        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", true)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b kK - 8 24", false)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b Q - 0 1", false)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", true)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13", true)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", true)]
        public void TestWhiteCastledFromFen(string fen, bool whiteCastled)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.WhiteCastled.Should().Be(whiteCastled);
        }

        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33", true)]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24", true)]
        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b q - 0 1", false)]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1", true)]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w Q - 4 13", true)]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13", false)]
        public void TestCanBlackCastledFromFen(string fen, bool blackCastled)
        {
            var board = new FenBoardFactory(new FenHelper(), fen, _logger).CreateBoard();
            board.BlackCastled.Should().Be(blackCastled);
        }
    }
}
