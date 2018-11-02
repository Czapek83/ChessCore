using NUnit.Framework;
using ChessEngine.Engine;
using FluentAssertions;

namespace ChessCoreEngine.Tests
{
    [TestFixture]
    public class MoveArraysTests
    {
        static readonly byte h2field = 63 - 8;
        static readonly byte h4field = 63 - 8 - 8 - 8;
        static readonly byte h3field = 63 - 8 - 8;
        static readonly byte g3field = 63 - 8 - 8 - 1;

        static readonly byte d4field = 63 - 8 - 8 - 8 - 4;
        static readonly byte d5field = 63 - 8 - 8 - 8 - 8 - 4;
        static readonly byte c5field = 63 - 8 - 8 - 8 - 8 - 5;
        static readonly byte e5field = 63 - 8 - 8 - 8 - 8 - 3;

        static readonly byte a3field = 63 - 8 - 8 - 7;
        static readonly byte a4field = 63 - 8 - 8 - 8 - 7;
        static readonly byte b4field = 63 - 8 - 8 - 8 - 6;

        static readonly byte d2field = 63 - 8 - 4;
        static readonly byte d3field = 63 - 8 - 8 - 4;
        static readonly byte e3field = 63 - 8 - 8 - 3;
        static readonly byte c3field = 63 - 8 - 8 - 5;

        //Move Arrays contains every possible move for every piece from every field.

        [Test]
        public void WhitePawnMoveArrays()
        {
            //Check all nulls
            for (int i = 0; i < 7; i++)
            {
                MoveArrays.WhitePawnMoves[i].Moves.Should().BeNullOrEmpty();
                MoveArrays.WhitePawnTotalMoves[i].Should().Be(0);
            }

            for (int i = 56; i < 64; i++)
            {
                MoveArrays.WhitePawnMoves[i].Moves.Should().BeNullOrEmpty();
                MoveArrays.WhitePawnTotalMoves[i].Should().Be(0);
            }

            //Find some random pawn fields and assert its values
            //h2 pawn has three moves (h3, g3, h4)
            MoveArrays.WhitePawnMoves[h2field].Moves.Should().Contain(h3field);
            MoveArrays.WhitePawnMoves[h2field].Moves.Should().Contain(h4field);
            MoveArrays.WhitePawnMoves[h2field].Moves.Should().Contain(g3field);
            MoveArrays.WhitePawnTotalMoves[h2field].Should().Be(3);
            //d4 pawn has three moves (d5, c5, e5)
            MoveArrays.WhitePawnMoves[d4field].Moves.Should().Contain(d5field);
            MoveArrays.WhitePawnMoves[d4field].Moves.Should().Contain(c5field);
            MoveArrays.WhitePawnMoves[d4field].Moves.Should().Contain(e5field);
            MoveArrays.WhitePawnTotalMoves[d4field].Should().Be(3);
            //a3 pawn has two moves (a4, b4)
            MoveArrays.WhitePawnMoves[a3field].Moves.Should().Contain(a4field);
            MoveArrays.WhitePawnMoves[a3field].Moves.Should().Contain(b4field);
            MoveArrays.WhitePawnTotalMoves[a3field].Should().Be(2);
            //d2 pawn has four moves (d3, d4, c3, e3)
            MoveArrays.WhitePawnMoves[d2field].Moves.Should().Contain(d3field);
            MoveArrays.WhitePawnMoves[d2field].Moves.Should().Contain(d4field);
            MoveArrays.WhitePawnMoves[d2field].Moves.Should().Contain(c3field);
            MoveArrays.WhitePawnMoves[d2field].Moves.Should().Contain(e3field);
            MoveArrays.WhitePawnTotalMoves[d2field].Should().Be(4);
        }
    }
}
