using ChessEngine.Engine;
using FluentAssertions;
using NUnit.Framework;

namespace ChessEngine.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void TestBoardDefaultConstructor()
        {
            var newGameBoard = new NewGameBoardFactory().CreateBoard();

            newGameBoard.Squares[4].Piece.PieceColor.Should().Be(ChessPieceColor.Black);
            newGameBoard.Squares[4].Piece.PieceType.Should().Be(ChessPieceType.King);

            newGameBoard.Squares[60].Piece.PieceColor.Should().Be(ChessPieceColor.White);
            newGameBoard.Squares[60].Piece.PieceType.Should().Be(ChessPieceType.King);
        }

        [Test]
        public void TestBoardExampleFenConstructor()
        {
            var boardFromFen = new FenBoardFactory("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 41").CreateBoard();

            boardFromFen.Squares[5].Piece.PieceColor.Should().Be(ChessPieceColor.Black);
            boardFromFen.Squares[5].Piece.PieceType.Should().Be(ChessPieceType.King);

            boardFromFen.Squares[8].Piece.Should().Be(null);

            boardFromFen.Squares[62].Piece.PieceColor.Should().Be(ChessPieceColor.White);
            boardFromFen.Squares[62].Piece.PieceType.Should().Be(ChessPieceType.King);
        }

        [Test]
        public void TestBoardEmptyConstructor()
        {
            var emptyGameBoard = new EmptyBoardFactory().CreateBoard();

            foreach (var square in emptyGameBoard.Squares)
                square.Piece.Should().Be(null);
            
        }

        [Test]
        public void StartupPosition()
        {
            var board = new NewGameBoardFactory().CreateBoard();
            board.InsufficientMaterial.Should().Be(false);
        }

        [TestCase("5k2/8/8/8/8/8/6N1/6K1 b - - 0 1", "WhiteKingAndKnightOnly")]
        [TestCase("5k2/8/8/8/8/8/5B2/6K1 b - - 0 1", "WhiteKingAndBishopOnly")]
        [TestCase("5k2/8/8/8/8/8/8/6K1 b - - 0 1", "KingsOnly")]
        [TestCase("5k2/5n2/8/8/8/8/5N2/6K1 b - - 0 1", "TwoKnightsOppositeOnly")]
        [TestCase("5k2/8/8/8/8/8/4NN2/6K1 b - - 0 1", "TwoKnightsWhiteOnly")]
        [TestCase("5k2/4nn2/8/8/8/8/8/6K1 b - - 0 1", "TwoKnightsBlackOnly")]
        [TestCase("5k2/5b2/8/8/8/8/5B2/6K1 b - - 0 1", "TwoBishopsOppositeOnly")]
        public void InsufficientMaterialTests(string fen, string because)
        {
            var board = new FenBoardFactory(fen).CreateBoard();
            board.InsufficientMaterial.Should().Be(true, because);
        }

        [TestCase("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b - - 0 1")]
        [TestCase("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b - - 8 24")]
        [TestCase("r2qkb1r/pp3pp1/2p1pn1p/4n3/3P3P/3Q2N1/PPPB1PP1/2KR3R w kq - 0 13")]
        [TestCase("r2q1rk1/1p3ppp/p1npb3/3Np1b1/4P3/1N1Q4/PPP1BPPP/R4RK1 w - - 4 13")]
        [TestCase("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33")]
        [TestCase("5k2/4bb2/8/8/8/8/8/6K1 b - - 0 1")]
        public void NotInsufficientMaterialTests(string fen)
        {
            var board = new FenBoardFactory(fen).CreateBoard();
            board.InsufficientMaterial.Should().Be(false);
        }
    }
}
