using ChessEngine.Engine;
using ChessEngine.Engine.Enums;
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
            var newGameBoard = new BoardFactory().CreateNewGameBoard();

            newGameBoard.Squares[4].Piece.PieceColor.Should().Be(ChessPieceColor.Black);
            newGameBoard.Squares[4].Piece.PieceType.Should().Be(ChessPieceType.King);

            newGameBoard.Squares[60].Piece.PieceColor.Should().Be(ChessPieceColor.White);
            newGameBoard.Squares[60].Piece.PieceType.Should().Be(ChessPieceType.King);
        }

        [Test]
        public void TestBoardEmptyConstructor()
        {
            var emptyGameBoard = new BoardFactory().CreateEmptyBoard();

            foreach (var square in emptyGameBoard.Squares)
                square.Piece.Should().Be(null);
            
        }

        [Test]
        public void StartupPosition()
        {
            var board = new BoardFactory().CreateNewGameBoard();
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
            var board = new BoardFactory().CreateBoardFromFen(fen);
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
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.InsufficientMaterial.Should().Be(false);
        }

        [TestCase("5k2/8/8/8/8/8/6N1/6K1 b - - 0 17")]
        [TestCase("5k2/8/8/8/8/8/5B2/6K1 b - - 0 45")]
        [TestCase("5k2/8/8/8/8/8/8/6K1 b - - 0 111")]
        [TestCase("5k2/5n2/8/8/8/8/5N2/6K1 b - - 5 97")]
        [TestCase("5k2/4nn2/8/8/8/8/8/6K1 b - - 0 45")]
        [TestCase("5k2/5b2/8/8/8/8/5B2/6K1 b - - 5 42")]
        public void EndgameTests(string fen)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.EndGamePhase.Should().Be(true);
        }


        [TestCase("r4k1r/8/4q3/8/8/8/4NNB1/R1B3K1 b - - 0 1")]
        [TestCase("r5k1/5pp1/1pNp2bp/1P1P4/2q5/1R3P1P/3r2PK/3Q4 w - - 0 40")]
        [TestCase("rn1r2k1/pp2bppp/6q1/3pP1N1/8/2N1B3/PP1Q1PbP/R4RK1 w - - 0 16")]
        [TestCase("r2r2k1/2q1bppp/2p1p3/pp1n1b2/2nP4/2PNBN1P/PP2BPP1/R1Q2RK1 b - - 5 17")]
        [TestCase("r2q1k2/3p1p2/p1p5/1p6/3P3Q/2P2PP1/PP3P2/R5K1 b - - 0 27")]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2")]
        [TestCase("rnbqkbnr/1p1p1ppp/p7/4p3/4P3/5N2/PPP2PPP/RNBQKB1R b KQkq - 1 5")]
        public void NotEndgameTests(string fen)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.EndGamePhase.Should().Be(false);
        }

        [TestCase("r4k1r/8/4q3/8/8/8/4NNB1/R1B3K1 b - - 0 1", 62)]
        [TestCase("r5k1/5pp1/1pNp2bp/1P1P4/2q5/1R3P1P/3r2PK/3Q4 w - - 0 40", 55)]
        [TestCase("rn1r2k1/pp2bppp/6q1/3pP1N1/8/2N1B3/PP1Q1PbP/R4RK1 w - - 0 16", 62)]
        [TestCase("r2r2k1/2q1bppp/2p1p3/pp1n1b2/2nP4/2PNBN1P/PP2BPP1/R1Q2RK1 b - - 5 17", 62)]
        [TestCase("r2q1k2/3p1p2/p1p5/1p6/3P3Q/2P2PP1/PP3P2/R5K1 b - - 0 27", 62)]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", 60)]
        [TestCase("rnbqkbnr/1p1p1ppp/p7/4p3/4P3/5N2/PPP2PPP/RNBQKB1R b KQkq - 1 5", 60)]
        public void WhiteKingPositionTests(string fen, byte whiteKingPosition)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.GetKingPosition(ChessPieceColor.White).Should().Be(whiteKingPosition);
        }

        [TestCase("r4k1r/8/4q3/8/8/8/4NNB1/R1B3K1 b - - 0 1", 5)]
        [TestCase("r5k1/5pp1/1pNp2bp/1P1P4/2q5/1R3P1P/3r2PK/3Q4 w - - 0 40", 6)]
        [TestCase("rn1r2k1/pp2bppp/6q1/3pP1N1/8/2N1B3/PP1Q1PbP/R4RK1 w - - 0 16", 6)]
        [TestCase("r2r2k1/2q1bppp/2p1p3/pp1n1b2/2nP4/2PNBN1P/PP2BPP1/R1Q2RK1 b - - 5 17", 6)]
        [TestCase("r2q1k2/3p1p2/p1p5/1p6/3P3Q/2P2PP1/PP3P2/R5K1 b - - 0 27", 5)]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", 4)]
        [TestCase("rnbqkbnr/1p1p1ppp/p7/4p3/4P3/5N2/PPP2PPP/RNBQKB1R b KQkq - 1 5", 4)]
        public void BlackKingPositionTests(string fen, byte blackKingPosition)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.GetKingPosition(ChessPieceColor.Black).Should().Be(blackKingPosition);
        }
        
        [TestCase("r4rk1/ppp3p1/8/8/1P6/P3BpPp/1RP1qK1P/5R2 w - - 4 28", true)]
        [TestCase("r4rk1/ppp3p1/8/8/1Pq5/PR2BpPp/2P2K1P/5R2 w - - 2 27", false)]
        [TestCase("8/5p2/5k2/6p1/5KPp/7P/8/8 w - g6 0 52", true)]
        [TestCase("5rk1/1p1b1p1p/p2P3r/4P1p1/2pR1N2/2P4P/PP5K/8 w - g6 0 37", false)]
        [TestCase("5rk1/1p1b1p1p/p2P3r/4P1p1/2pR1N2/2P4P/PP5K/8 w - g6 0 37", false)]
        [TestCase("4kb1r/1p1b1ppp/p5r1/3NPn2/2pP4/7P/PPP2B2/R4RK1 w k - 2 23", true)]
        public void WhiteIsCheckedTest(string fen, bool whiteIsChecked)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.GenerateValidMoves();
            board.IsChecked(ChessPieceColor.White).Should().Be(whiteIsChecked);
        }

        [TestCase("4kb1r/1p1b1ppp/p5r1/3NPn2/2pP4/7P/PPP2B2/R4RK1 w k - 2 23", false)]
        [TestCase("8/1p6/p2p1b2/5k1p/3N2p1/1P4K1/P1P1N3/8 b - - 5 37", true)]
        [TestCase("8/1p6/p2p1b2/7p/3Nk1p1/1P4K1/P1P1N3/8 w - - 6 38", false)]
        [TestCase("4k3/N7/2p4p/8/2P3p1/8/PP4PP/6K1 b - - 0 36", false)]
        [TestCase("rnbq1bnr/pppp1ppp/8/2k1p3/3PP3/5N2/PPP1BPPP/RNBQ1RK1 b - d3 0 5", true)]
        public void BlackIsCheckedTest(string fen, bool blackIsChecked)
        {
            var board = new BoardFactory().CreateBoardFromFen(fen);
            board.GenerateValidMoves();
            board.IsChecked(ChessPieceColor.Black).Should().Be(blackIsChecked);
        }
    }
}
