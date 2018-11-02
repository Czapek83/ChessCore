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
    }
}
