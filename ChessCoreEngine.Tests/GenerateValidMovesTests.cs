using ChessEngine.Engine;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class GenerateValidMovesTests
    {
        [Test]
        public void NewGame_NonePiecesCanMove_Excluding_KnightsAndPawns()
        {
            var board = Board.CreateNewGameBoard();
            board.GenerateValidMoves();

            //only knights and pawns have any moves

            //black
            board.GetPiece(0).ValidMoves.Should().BeEmpty();
            board.GetPiece(2).ValidMoves.Should().BeEmpty();
            board.GetPiece(3).ValidMoves.Should().BeEmpty();
            board.GetPiece(4).ValidMoves.Should().BeEmpty();
            board.GetPiece(5).ValidMoves.Should().BeEmpty();
            board.GetPiece(7).ValidMoves.Should().BeEmpty();
            //white
            board.GetPiece(56).ValidMoves.Should().BeEmpty();
            board.GetPiece(58).ValidMoves.Should().BeEmpty();
            board.GetPiece(59).ValidMoves.Should().BeEmpty();
            board.GetPiece(60).ValidMoves.Should().BeEmpty();
            board.GetPiece(61).ValidMoves.Should().BeEmpty();
            board.GetPiece(63).ValidMoves.Should().BeEmpty();
        }

        [Test]
        public void NewGame_KnightsHaveMoves()
        {
            var board = Board.CreateNewGameBoard();
            board.GenerateValidMoves();

            //only knights and pawns have any moves

            //black
            board.GetPiece(1).ValidMoves.Should().HaveCount(2);
            board.GetPiece(6).ValidMoves.Should().HaveCount(2);
            board.GetPiece(1).ValidMoves.Should().Contain(1 + 16 - 1);
            board.GetPiece(1).ValidMoves.Should().Contain(1 + 16 + 1);
            board.GetPiece(6).ValidMoves.Should().Contain(6 + 16 - 1);
            board.GetPiece(6).ValidMoves.Should().Contain(6 + 16 + 1);

            //white
            board.GetPiece(57).ValidMoves.Should().HaveCount(2);
            board.GetPiece(62).ValidMoves.Should().HaveCount(2);
            board.GetPiece(57).ValidMoves.Should().Contain(57 - 16 - 1);
            board.GetPiece(57).ValidMoves.Should().Contain(57 - 16 + 1);
            board.GetPiece(62).ValidMoves.Should().Contain(62 - 16 - 1);
            board.GetPiece(62).ValidMoves.Should().Contain(62 - 16 + 1);
        }

        [Test]
        public void NewGame_PawnsHaveMoves()
        {
            var board = Board.CreateNewGameBoard();
            board.GenerateValidMoves();

            //black pawns
            for (byte i = 8; i < 8 + 8; i++)
            {
                board.GetPiece(i).ValidMoves.Should().HaveCount(2);
                board.GetPiece(i).ValidMoves.Should().Contain((byte)(i+8));
                board.GetPiece(i).ValidMoves.Should().Contain((byte)(i + 16));
            }

            //white pawns
            for (byte i = 63-8; i > 63-8-8; i--)
            {
                board.GetPiece(i).ValidMoves.Should().HaveCount(2);
                board.GetPiece(i).ValidMoves.Should().Contain((byte)(i - 8));
                board.GetPiece(i).ValidMoves.Should().Contain((byte)(i - 16));
            }
        }

        [Test]
        public void SpecificPositionQueenMoves()
        {
            var board = Board.CreateBoardFromFen("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35");
            board.GenerateValidMoves();

            var blackQueen = board.GetPiece(15);

            blackQueen.ValidMoves.Should().HaveCount(11);

            for (byte i=11; i<=12; i++)
                blackQueen.ValidMoves.Should().Contain(i);

            blackQueen.ValidMoves.Should().Contain(7);

            //Diagonal moves
            for (byte i = 22; i <= 53; i+=7)
            {
                blackQueen.ValidMoves.Should().Contain(i);
            }
        }

        [Test]
        public void SpecificPositionKingMoves()
        {
            var board = Board.CreateBoardFromFen("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35");
            board.GenerateValidMoves();

            var blackKing = board.GetPiece(6);

            blackKing.ValidMoves.Should().HaveCount(2);
            blackKing.ValidMoves.Should().Contain(5);
            blackKing.ValidMoves.Should().Contain(7);
        }

        [Test]
        public void SpecificPositionRookMoves()
        {
            var board = Board.CreateBoardFromFen("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35");
            board.GenerateValidMoves();

            var blackRook = board.GetPiece(0);

            blackRook.ValidMoves.Should().HaveCount(6);
            blackRook.ValidMoves.Should().Contain(8);

            for (byte i=1; i<=5; i++)
            {
                blackRook.ValidMoves.Should().Contain(i);
            }
        }

        [Test]
        public void SpecificPositionPawnMove()
        {
            var board = Board.CreateBoardFromFen("r5k1/2R4q/p6p/8/3P2P1/4P3/2P4K/8 w - - 0 35");
            board.GenerateValidMoves();

            var blackPawn = board.GetPiece(16);

            blackPawn.ValidMoves.Should().HaveCount(1);
            blackPawn.ValidMoves.Should().Contain(24);

        }

        [Test]
        public void SpecificPositionBishopMoves()
        {
            var board = Board.CreateBoardFromFen("r5k1/5pp1/1pNp2bp/1P1P4/2q5/1R3P1P/3r2PK/3Q4 w - - 0 40");

            board.GenerateValidMoves();

            var blackBishop = board.GetPiece(41);

            for (byte i = 41-7; i >= 1; i-=7)
            {
                blackBishop.ValidMoves.Contains(i);
            }

            blackBishop.ValidMoves.Contains(15);
            blackBishop.ValidMoves.Contains(31);
        }

        [Test]
        public void SpecificPositionPawnMoveOrCapture()
        {
            var board = Board.CreateBoardFromFen("3b4/8/p5p1/2pk1pp1/P5P1/1P1K1P1P/1B6/8 w - - 9 43");
            board.GenerateValidMoves();

            var blackPawn = board.GetPiece(29);

            blackPawn.ValidMoves.Should().HaveCount(2);
            blackPawn.ValidMoves.Should().Contain(38);
            blackPawn.ValidMoves.Should().Contain(37);
        }

        [Test]
        public void SpecificPositionPawnCapture()
        {
            var board = Board.CreateBoardFromFen("r2r2k1/2q2pp1/2p1p2p/pp1n1b2/2nP4/bPPNBN1P/P3BPP1/R1Q2RK1 w - - 1 19");
            board.GenerateValidMoves();

            var whitePawn = board.GetPiece(41);

            whitePawn.ValidMoves.Should().HaveCount(2);
            whitePawn.ValidMoves.Should().Contain(33);
            whitePawn.ValidMoves.Should().Contain(34);
        }

        [Test]
        public void SpecificPositionCastling()
        {
            var board = Board.CreateBoardFromFen("rnbqk2r/1p2bpp1/p2ppn1p/8/3NPP1B/2N2Q2/PPP3PP/R3KB1R b KQkq - 3 9");
            board.GenerateValidMoves();

            var blackKing = board.GetPiece(board.BlackKingPosition);

            blackKing.ValidMoves.Should().HaveCount(3);
            blackKing.ValidMoves.Should().Contain(5);
            blackKing.ValidMoves.Should().Contain(6);
            blackKing.ValidMoves.Should().Contain(11);
        }

        [Test]
        public void SpecificPositionEnPassant()
        {
            var board = Board.CreateBoardFromFen("rnbqkbnr/ppp3pp/3p4/4ppP1/4P3/8/PPPP1P1P/RNBQKBNR w KQkq f6 0 4");
            board.GenerateValidMoves();

            var whitePawn = board.GetPiece(30);

            whitePawn.ValidMoves.Should().HaveCount(2);
            whitePawn.ValidMoves.Should().Contain(22);
            whitePawn.ValidMoves.Should().Contain(21);
        }

        [Test]
        public void SpecificPositionWhiteKingInFrontOfOther_BlacksMove()
        {
            //When there is black's move...
            //White's king moves can be made in front of opposite king, but not under opposite pawn's capture field
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 b - - 2 9");
            board.GenerateValidMoves();

            var whiteKing = board.GetPiece(board.WhiteKingPosition);

            whiteKing.ValidMoves.Should().HaveCount(7);
            whiteKing.ValidMoves.Should().Contain(27);
            whiteKing.ValidMoves.Should().Contain(28);
            whiteKing.ValidMoves.Should().Contain(35);
            whiteKing.ValidMoves.Should().Contain(37);
            whiteKing.ValidMoves.Should().Contain(43);
            whiteKing.ValidMoves.Should().Contain(44);
            whiteKing.ValidMoves.Should().Contain(45);
        }

        [Test]
        public void SpecificPositionBlackKingInFrontOfOther_BlacksMove()
        {
            //When there is black's move...
            //Black's king moves cannot be made in front of opposite king
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 b - - 2 9");
            board.GenerateValidMoves();

            var blackKing = board.GetPiece(board.BlackKingPosition);

            blackKing.ValidMoves.Should().HaveCount(5);
            blackKing.ValidMoves.Should().Contain(11);
            blackKing.ValidMoves.Should().Contain(12);
            blackKing.ValidMoves.Should().Contain(13);
            blackKing.ValidMoves.Should().Contain(19);
            blackKing.ValidMoves.Should().Contain(21);
        }

        [Test]
        public void SpecificPositionWhiteKingInFrontOfOther_WhitesMove()
        {
            //When there is white's move...
            //White's king moves cannote be made in front of opposite king, nor black pawn's capture field
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 w - - 2 9");
            board.GenerateValidMoves();

            var whiteKing = board.GetPiece(board.WhiteKingPosition);

            whiteKing.ValidMoves.Should().HaveCount(5);
            whiteKing.ValidMoves.Should().Contain(35);
            whiteKing.ValidMoves.Should().Contain(37);
            whiteKing.ValidMoves.Should().Contain(43);
            whiteKing.ValidMoves.Should().Contain(44);
            whiteKing.ValidMoves.Should().Contain(45);
        }

        [Test]
        public void SpecificPositionBlackKingInFrontOfOther_WhitesMove()
        {
            //When there is white's move...
            //Black's king moves are possible to made in front of opposite king
            var board = Board.CreateBoardFromFen("8/8/4k1p1/8/4K3/6P1/8/8 w - - 2 9");
            board.GenerateValidMoves();

            var blackKing = board.GetPiece(board.BlackKingPosition);

            blackKing.ValidMoves.Should().HaveCount(8);
            blackKing.ValidMoves.Should().Contain(11);
            blackKing.ValidMoves.Should().Contain(12);
            blackKing.ValidMoves.Should().Contain(13);
            blackKing.ValidMoves.Should().Contain(19);
            blackKing.ValidMoves.Should().Contain(21);
            blackKing.ValidMoves.Should().Contain(27);
            blackKing.ValidMoves.Should().Contain(28);
            blackKing.ValidMoves.Should().Contain(29);
        }
    }
}
