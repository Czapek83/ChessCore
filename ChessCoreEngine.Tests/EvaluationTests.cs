﻿using ChessEngine.Engine;
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

        #region PieceScoreEvaluationTests
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
            var piece = new Pawn(ChessPieceColor.White);
            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];
            var score = piece.EvaluatePieceScore(position, isEndgame, whitePawnCount, blackPawnCount);

            score.Should().Be(expectedScore);
            whitePawnCount[whitePawnCountIndex].Should().Be(whitePawnCountValue);
            blackPawnCount.Should().OnlyContain(x => x == 0);
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
            var piece = new Pawn(ChessPieceColor.White);
            
            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];
            piece.EvaluatePieceScore(28, isEndgame, whitePawnCount, blackPawnCount);
            var score = piece.EvaluatePieceScore(position, isEndgame, whitePawnCount, blackPawnCount);

            score.Should().Be(expectedScore);
            whitePawnCount[whitePawnCountIndex].Should().Be(whitePawnCountValue);
            blackPawnCount.Should().OnlyContain(x => x == 0);
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
            var piece = new Pawn(ChessPieceColor.Black);
            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];
            var score = piece.EvaluatePieceScore(position, isEndgame, whitePawnCount, blackPawnCount);

            score.Should().Be(expectedScore);
            blackPawnCount[blackPawnCountIndex].Should().Be(blackPawnCountValue);
            whitePawnCount.Should().OnlyContain(x => x == 0);
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
            var piece = new Pawn(ChessPieceColor.Black);

            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];
            piece.EvaluatePieceScore(35, isEndgame, whitePawnCount, blackPawnCount);
            var score = piece.EvaluatePieceScore(position, isEndgame, whitePawnCount, blackPawnCount);

            score.Should().Be(expectedScore);
            blackPawnCount[blackPawnCountIndex].Should().Be(blackPawnCountValue);
            whitePawnCount.Should().OnlyContain(x => x == 0);
        }

        [Test]
        public void EvaluatePieceScores_Knight()
        {
            for (byte position = 0; position < Knight.KnightTable.Length; position++)
            {
                var whitePiece = new Knight(ChessPieceColor.White);
                var blackPawnCount = new short[8];
                var whitePawnCount = new short[8];

                var whitePieceEndgameScore = whitePiece.EvaluatePieceScore(position, true, whitePawnCount, blackPawnCount);
                var whitePieceNotEndgameScore = whitePiece.EvaluatePieceScore(position, false, whitePawnCount, blackPawnCount);
                whitePieceEndgameScore.Should().Be(320+Knight.KnightTable[position] - 10);
                whitePieceNotEndgameScore.Should().Be(320 + Knight.KnightTable[position]);

                byte blackPosition = (byte) (63 - position);

                var blackPiece = new Knight(ChessPieceColor.Black);
                var blackPieceEndgameScore = blackPiece.EvaluatePieceScore(blackPosition, true, whitePawnCount, blackPawnCount);
                var blackPieceNotEndgameScore = blackPiece.EvaluatePieceScore(blackPosition, false, whitePawnCount, blackPawnCount);
                blackPieceEndgameScore.Should().Be(320 + Knight.KnightTable[position] - 10);
                blackPieceNotEndgameScore.Should().Be(320 + Knight.KnightTable[position]);
            }
        }

        [Test]
        public void EvaluatePieceScores_Bishop()
        {
            for (byte position = 0; position < Bishop.BishopTable.Length; position++)
            {
                var blackPawnCount = new short[8];
                var whitePawnCount = new short[8];

                var whitePiece = new Bishop(ChessPieceColor.White);
                var whitePieceEndgameScore = whitePiece.EvaluatePieceScore(position, true, whitePawnCount, blackPawnCount);
                var whitePieceNotEndgameScore = whitePiece.EvaluatePieceScore(position, false, whitePawnCount, blackPawnCount);
                whitePieceEndgameScore.Should().Be(325 + Bishop.BishopTable[position] + 10);
                whitePieceNotEndgameScore.Should().Be(325 + Bishop.BishopTable[position]);

                byte blackPosition = (byte)(63 - position);

                var blackPiece = new Bishop(ChessPieceColor.Black);
                var blackPieceEndgameScore = blackPiece.EvaluatePieceScore(blackPosition, true, whitePawnCount, blackPawnCount);
                var blackPieceNotEndgameScore = blackPiece.EvaluatePieceScore(blackPosition, false, whitePawnCount, blackPawnCount);
                blackPieceEndgameScore.Should().Be(325 + Bishop.BishopTable[position] + 10);
                blackPieceNotEndgameScore.Should().Be(325 + Bishop.BishopTable[position]);
            }
        }

        [Test]
        public void EvaluatePieceScores_Queen()
        {
            var movedQueen = new Queen(ChessPieceColor.White);
            movedQueen.Moved = true;
            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];

            var movedQueenEndgameScore = movedQueen.EvaluatePieceScore(1, true, whitePawnCount, blackPawnCount);
            var movedQueenNotEndgameScore = movedQueen.EvaluatePieceScore(1, false, whitePawnCount, blackPawnCount);

            var notMovedQueen = new Queen(ChessPieceColor.White);
            var notMovedQueenEndgameScore = notMovedQueen.EvaluatePieceScore(1, true, whitePawnCount, blackPawnCount);
            var notMovedQueenNotEndgameScore = notMovedQueen.EvaluatePieceScore(1, false, whitePawnCount, blackPawnCount);

            movedQueenEndgameScore.Should().Be(975);
            movedQueenNotEndgameScore.Should().Be(975-10);
            notMovedQueenEndgameScore.Should().Be(975);
            notMovedQueenNotEndgameScore.Should().Be(975);
        }

        #endregion

        [TestCase("r4k1r/8/4q3/8/8/8/4NNB1/R1B3K1 b - - 0 1", -195)]
        [TestCase("r5k1/5pp1/1pNp2bp/1P1P4/2q5/1R3P1P/3r2PK/3Q4 w - - 0 40", -495)]
        [TestCase("rn1r2k1/pp2bppp/6q1/3pP1N1/8/2N1B3/PP1Q1PbP/R4RK1 w - - 0 16", -57)]
        [TestCase("r2r2k1/2q1bppp/2p1p3/pp1n1b2/2nP4/2PNBN1P/PP2BPP1/R1Q2RK1 b - - 5 17", 10)]
        [TestCase("r2q1k2/3p1p2/p1p5/1p6/3P3Q/2P2PP1/PP3P2/R5K1 b - - 0 27", 271)]
        [TestCase("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", 75)]
        [TestCase("rnbqkbnr/1p1p1ppp/p7/4p3/4P3/5N2/PPP2PPP/RNBQKB1R b KQkq - 1 5", 70)]
        [TestCase("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35", -680)]
        [TestCase("8/8/8/1p1p1kpp/3P4/1r3PPP/1P1R1K2/8 b - - 0 43", 150)]
        [TestCase("r1bqkbnr/1ppp1ppp/p1n5/4p3/B3P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 1 4", 20)]
        public void BoardEvaluationTest(string fen, int expectedScore)
        {
            var board = Board.CreateBoardFromFen(fen);
            var score = Evaluation.EvaluateBoardScore(board.GetEvaluationParameters(), board);

            score.Should().Be(expectedScore);
        }
    }
}
