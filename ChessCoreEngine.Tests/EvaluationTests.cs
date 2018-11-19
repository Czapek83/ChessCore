using ChessEngine.Engine;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class EvaluationTests
    {
        //Evaluation _systemUnderTests;

        [SetUp]
        public void SetUp()
        {

        }

        [TestCase(11, true, 150, 3, 60)]
        [TestCase(12, true, 150, 4, 60)]
        [TestCase(13, true, 150, 5, 60)]
        [TestCase(14, true, 150, 6, 60)]
        [TestCase(8, true, 135, 0, 60)]
        [TestCase(15, true, 135, 7, 60)]
        [TestCase(16, true, 105, 0, 35)]
        [TestCase(17, true, 120, 1, 35)]
        public void EvaluatePieceScore_WhitePawns(byte position, bool isEndgame,
            int expectedScore, int whitePawnCountIndex, short whitePawnCountValue)
        {
            var piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);
            Evaluation.blackPawnCount = new short[8];
            Evaluation.whitePawnCount = new short[8];
            var score = Evaluation.EvaluatePieceScore(piece, position, isEndgame);

            score.Should().Be(expectedScore);
            Evaluation.whitePawnCount[whitePawnCountIndex].Should().Be(whitePawnCountValue);
            Evaluation.blackPawnCount.Should().OnlyContain(x => x == 0);
        }

        [TestCase(11, true, 150, 3, 60)]
        [TestCase(12, true, 135, 4, 70)]
        [TestCase(13, true, 150, 5, 60)]
        [TestCase(14, true, 150, 6, 60)]
        [TestCase(8, true, 135, 0, 60)]
        [TestCase(15, true, 135, 7, 60)]
        [TestCase(16, true, 105, 0, 35)]
        [TestCase(17, true, 120, 1, 35)]
        public void EvaluatePieceScore_WhiteMultiplePawns(byte position, bool isEndgame,
            int expectedScore, int whitePawnCountIndex, short whitePawnCountValue)
        {
            var piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);
            
            Evaluation.blackPawnCount = new short[8];
            Evaluation.whitePawnCount = new short[8];
            Evaluation.EvaluatePieceScore(piece, 28, isEndgame);
            var score = Evaluation.EvaluatePieceScore(piece, position, isEndgame);

            score.Should().Be(expectedScore);
            Evaluation.whitePawnCount[whitePawnCountIndex].Should().Be(whitePawnCountValue);
            Evaluation.blackPawnCount.Should().OnlyContain(x => x == 0);
        }

        [TestCase(52, true, 150, 4, 60)]
        [TestCase(51, true, 150, 3, 60)]
        [TestCase(50, true, 150, 2, 60)]
        [TestCase(49, true, 150, 1, 60)]
        [TestCase(55, true, 135, 7, 60)]
        [TestCase(48, true, 135, 0, 60)]
        [TestCase(47, true, 105, 7, 35)]
        [TestCase(46, true, 120, 6, 35)]
        public void EvaluatePieceScore_BlackPawns(byte position, bool isEndgame,
            int expectedScore, int blackPawnCountIndex, short blackPawnCountValue)
        {
            var piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);
            Evaluation.blackPawnCount = new short[8];
            Evaluation.whitePawnCount = new short[8];
            var score = Evaluation.EvaluatePieceScore(piece, position, isEndgame);

            score.Should().Be(expectedScore);
            Evaluation.blackPawnCount[blackPawnCountIndex].Should().Be(blackPawnCountValue);
            Evaluation.whitePawnCount.Should().OnlyContain(x => x == 0);
        }

        [TestCase(52, true, 150, 4, 60)]
        [TestCase(51, true, 135, 3, 70)]
        [TestCase(50, true, 150, 2, 60)]
        [TestCase(49, true, 150, 1, 60)]
        [TestCase(55, true, 135, 7, 60)]
        [TestCase(48, true, 135, 0, 60)]
        [TestCase(47, true, 105, 7, 35)]
        [TestCase(46, true, 120, 6, 35)]
        public void EvaluatePieceScore_BlackMultiplePawns(byte position, bool isEndgame,
            int expectedScore, int blackPawnCountIndex, short blackPawnCountValue)
        {
            var piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);

            Evaluation.blackPawnCount = new short[8];
            Evaluation.whitePawnCount = new short[8];
            Evaluation.EvaluatePieceScore(piece, 35, isEndgame);
            var score = Evaluation.EvaluatePieceScore(piece, position, isEndgame);

            score.Should().Be(expectedScore);
            Evaluation.blackPawnCount[blackPawnCountIndex].Should().Be(blackPawnCountValue);
            Evaluation.whitePawnCount.Should().OnlyContain(x => x == 0);
        }

        [TestCase(0, true, 260, ChessPieceColor.White)]
        [TestCase(0, false, 270, ChessPieceColor.White)]
        [TestCase(11, false, 320, ChessPieceColor.White)]
        [TestCase(11, true, 310, ChessPieceColor.White)]
        [TestCase(63, true, 260, ChessPieceColor.Black)]
        [TestCase(63, false, 270, ChessPieceColor.Black)]
        [TestCase(52, false, 320, ChessPieceColor.Black)]
        [TestCase(52, true, 310, ChessPieceColor.Black)]
        public void EvaluatePieceScore_Knight(byte position, bool isEndgame,
            int expectedScore, ChessPieceColor chessPieceColor)
        {
            var piece = new Piece(ChessPieceType.Knight, chessPieceColor);

            var score = Evaluation.EvaluatePieceScore(piece, position, isEndgame);

            score.Should().Be(expectedScore);
        }


    }
}
