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

        [Test]
        public void EvaluatePieceScores_Knight()
        {
            for (byte position = 0; position < Evaluation.KnightTable.Length; position++)
            {
                var whitePiece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
                var whitePieceEndgameScore = Evaluation.EvaluatePieceScore(whitePiece, position, true);
                var whitePieceNotEndgameScore = Evaluation.EvaluatePieceScore(whitePiece, position, false);
                whitePieceEndgameScore.Should().Be(320+Evaluation.KnightTable[position] - 10);
                whitePieceNotEndgameScore.Should().Be(320 + Evaluation.KnightTable[position]);

                byte blackPosition = (byte) (63 - position);

                var blackPiece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
                var blackPieceEndgameScore = Evaluation.EvaluatePieceScore(blackPiece, blackPosition, true);
                var blackPieceNotEndgameScore = Evaluation.EvaluatePieceScore(blackPiece, blackPosition, false);
                blackPieceEndgameScore.Should().Be(320 + Evaluation.KnightTable[position] - 10);
                blackPieceNotEndgameScore.Should().Be(320 + Evaluation.KnightTable[position]);
            }
        }

        [Test]
        public void EvaluatePieceScores_Bishop()
        {
            for (byte position = 0; position < Evaluation.BishopTable.Length; position++)
            {
                var whitePiece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
                var whitePieceEndgameScore = Evaluation.EvaluatePieceScore(whitePiece, position, true);
                var whitePieceNotEndgameScore = Evaluation.EvaluatePieceScore(whitePiece, position, false);
                whitePieceEndgameScore.Should().Be(325 + Evaluation.BishopTable[position] + 10);
                whitePieceNotEndgameScore.Should().Be(325 + Evaluation.BishopTable[position]);

                byte blackPosition = (byte)(63 - position);

                var blackPiece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
                var blackPieceEndgameScore = Evaluation.EvaluatePieceScore(blackPiece, blackPosition, true);
                var blackPieceNotEndgameScore = Evaluation.EvaluatePieceScore(blackPiece, blackPosition, false);
                blackPieceEndgameScore.Should().Be(325 + Evaluation.BishopTable[position] + 10);
                blackPieceNotEndgameScore.Should().Be(325 + Evaluation.BishopTable[position]);
            }
        }
    }
}
