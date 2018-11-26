using ChessEngine.Engine;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class AttackBoardsTests
    {

        [Test]
        public void NewGame()
        {
            var board = Board.CreateNewGameBoard();
            board.GenerateValidMoves();

            var expectedWhiteBoard = new bool[64];
            for (int i = 40; i < 63; i++)
                if (i != 56)
                    expectedWhiteBoard[i] = true;

            var expectedBlackBoard = new bool[64];
            for (int i = 1; i <= 23; i++)
                if (i != 7)
                    expectedBlackBoard[i] = true;


            for (int i = 0; i < 64; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
            for (int i = 0; i < 64; i++)
            {
                board.BlackAttackBoard[i].Should().Be(expectedBlackBoard[i], $"Error on {i}");
            }
        }

        //TODO: Test also scenario when kings are near to each other
        [Test]
        public void SpecificPosition1()
        {
            var board = Board.CreateBoardFromFen("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35");
            board.GenerateValidMoves();

            
            var expectedWhiteBoard = new bool[64];
            //Rook horizontal
            for (int i = 8; i < 16; i++)
                if (i != 10)
                    expectedWhiteBoard[i] = true;
            //Rook vertical
            for (int i = 2; i <=50; i+=8)
                if (i != 10)
                    expectedWhiteBoard[i] = true;
            //pawns
            expectedWhiteBoard[43] = true;
            expectedWhiteBoard[41] = true;
            expectedWhiteBoard[28] = true;
            expectedWhiteBoard[29] = true;
            expectedWhiteBoard[31] = true;
            expectedWhiteBoard[35] = true;
            expectedWhiteBoard[37] = true;
            //king
            expectedWhiteBoard[63] = true;
            expectedWhiteBoard[62] = true;
            expectedWhiteBoard[54] = true;
            expectedWhiteBoard[47] = true;
            expectedWhiteBoard[46] = true;


            var expectedBlackBoard = new bool[64];
            for (int i = 1; i <= 16; i++)
                if (i != 9)
                    expectedBlackBoard[i] = true;

            //pawns
            expectedBlackBoard[25] = true;
            expectedBlackBoard[30] = true;

            //queen
            expectedBlackBoard[22] = true;
            expectedBlackBoard[23] = true;
            expectedBlackBoard[29] = true;
            expectedBlackBoard[36] = true;
            expectedBlackBoard[43] = true;
            expectedBlackBoard[50] = true;

            for (int i = 0; i < 64; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
            for (int i = 0; i < 64; i++)
            {
                board.BlackAttackBoard[i].Should().Be(expectedBlackBoard[i], $"Error on {i}");
            }
        }



        [Test]
        public void SpecificPosition2_WhiteBoard()
        {
            var board = Board.CreateBoardFromFen("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33");
            board.GenerateValidMoves();


            var expectedWhiteBoard = new bool[64];
            expectedWhiteBoard[9] = true;
            expectedWhiteBoard[17] = true;
            expectedWhiteBoard[19] = true;

            //Rook horizontal
            for (int i = 24; i <= 63; i++)
                if (i != 27 && i != 28 && i != 30 && i != 33 && i != 37 && i != 39 && i != 48 && i != 50 && i != 53)
                    expectedWhiteBoard[i] = true;



            for (int i = 0; i <= 63; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
        }

        [Test]
        public void SpecificPosition2_BlackBoard()
        {
            var board = Board.CreateBoardFromFen("3r4/1p1r2kp/p1q3p1/2P1p3/1Q1p4/3RnPN1/PP4PP/3R3K b - - 3 33");
            board.GenerateValidMoves();


            var expectedBlackBoard = new bool[64];
            

            //Rook horizontal
            for (int i = 0; i <= 38; i++)
                if (i != 8 && i != 24 && i != 28 && i != 30 && i != 33)
                    expectedBlackBoard[i] = true;


            expectedBlackBoard[42] = true;
            expectedBlackBoard[44] = true;
            expectedBlackBoard[45] = true;
            expectedBlackBoard[50] = true;
            expectedBlackBoard[54] = true;
            expectedBlackBoard[59] = true;
            expectedBlackBoard[61] = true;


            for (int i = 0; i <= 63; i++)
            {
                board.BlackAttackBoard[i].Should().Be(expectedBlackBoard[i], $"Error on {i}");
            }
        }

        
        [Test]
        public void SpecificPosition3_WhiteBoard()
        {
            var board = Board.CreateBoardFromFen("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b kK - 8 24");
            board.GenerateValidMoves();

            var expectedWhiteBoard = new bool[64];
            expectedWhiteBoard[19] = true;
            expectedWhiteBoard[21] = true;
            expectedWhiteBoard[19] = true;

            //Rook horizontal
            for (int i = 25; i <= 63; i++)
                if (i != 30 && i != 31 && i != 35 && i != 37 && i != 39 && i != 40 && i != 49 && i != 50 && i != 60)
                    expectedWhiteBoard[i] = true;



            for (int i = 0; i <= 63; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
        }

        [Test]
        public void SpecificPosition3_BlackBoard()
        {
            var board = Board.CreateBoardFromFen("4r1k1/2p3pp/p1ppqp2/2n5/3QP3/1PN2P2/P1P3PP/4R1K1 b kK - 8 24");
            board.GenerateValidMoves();


            var expectedBlackBoard = new bool[64];


            //Rook horizontal
            for (int i = 0; i <= 38; i++)
                if (i != 8 && i != 10 && i != 18 && i != 24 && i != 31 && i != 33 && i != 35 && i != 37)
                    expectedBlackBoard[i] = true;


            expectedBlackBoard[41] = true;
            expectedBlackBoard[43] = true;
            expectedBlackBoard[47] = true;

            for (int i = 0; i <= 63; i++)
            {
                board.BlackAttackBoard[i].Should().Be(expectedBlackBoard[i], $"Error on {i}");
            }
        }

        [Test]
        public void SpecificPosition4_WhiteBoard()
        {
            var board = Board.CreateBoardFromFen("5k2/8/6p1/R1B2p2/3b4/1r4P1/5P2/6K1 b Q - 0 1");
            board.GenerateValidMoves();

            var expectedWhiteBoard = new bool[64];
            
            //Rook horizontal
            for (int i = 0; i <= 56; i+=8)
                if (i != 24)
                    expectedWhiteBoard[i] = true;

            expectedWhiteBoard[17] = true;
            expectedWhiteBoard[25] = true;
            expectedWhiteBoard[33] = true;
            expectedWhiteBoard[26] = true;
            expectedWhiteBoard[19] = true;
            expectedWhiteBoard[12] = true;
            expectedWhiteBoard[5] = true;
            expectedWhiteBoard[35] = true;
            expectedWhiteBoard[37] = true;
            expectedWhiteBoard[39] = true;
            expectedWhiteBoard[44] = true;
            expectedWhiteBoard[46] = true;
            expectedWhiteBoard[53] = true;
            expectedWhiteBoard[54] = true;
            expectedWhiteBoard[55] = true;
            expectedWhiteBoard[61] = true;
            expectedWhiteBoard[63] = true;


            for (int i = 0; i <= 63; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
        }

        [Test]
        public void KingsInFrontOfEachOther_BlackBoard()
        {
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 b - - 2 9");
            board.GenerateValidMoves();

            var expectedBlackBoard = new bool[64];

            expectedBlackBoard[11] = true;
            expectedBlackBoard[12] = true;
            expectedBlackBoard[13] = true;
            expectedBlackBoard[19] = true;
            expectedBlackBoard[21] = true;
            expectedBlackBoard[27] = true;
            expectedBlackBoard[28] = true;
            expectedBlackBoard[29] = true;
            expectedBlackBoard[31] = true;

            for (int i = 0; i <= 63; i++)
            {
                board.BlackAttackBoard[i].Should().Be(expectedBlackBoard[i], $"Error on {i}");
            }
        }

        [Test]
        public void KingsInFrontOfEachOther_WhiteBoard()
        {
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 b - - 2 9");
            board.GenerateValidMoves();

            var expectedWhiteBoard = new bool[64];

            expectedWhiteBoard[27] = true;
            expectedWhiteBoard[28] = true;
            expectedWhiteBoard[29] = true;
            expectedWhiteBoard[35] = true;
            expectedWhiteBoard[37] = true;
            expectedWhiteBoard[39] = true;
            expectedWhiteBoard[43] = true;
            expectedWhiteBoard[44] = true;
            expectedWhiteBoard[45] = true;
            
            for (int i = 0; i <= 63; i++)
            {
                board.WhiteAttackBoard[i].Should().Be(expectedWhiteBoard[i], $"Error on {i}");
            }
        }

    }
}
