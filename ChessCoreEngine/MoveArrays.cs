using System.Collections.Generic;

namespace ChessEngine.Engine
{
    internal struct PieceMoveSet
    {
        internal readonly List<byte> Moves;

        internal PieceMoveSet(List<byte> moves)
        {
            Moves = moves;
        }
    }

    internal static class MoveArrays
    {
        internal static PieceMoveSet[] BishopMoves1;
        
        internal static PieceMoveSet[] BishopMoves2;

        internal static PieceMoveSet[] BishopMoves3;

        internal static PieceMoveSet[] BishopMoves4;

        internal static PieceMoveSet[] BlackPawnMoves;

        internal static PieceMoveSet[] WhitePawnMoves;

        internal static PieceMoveSet[] KnightMoves;

        internal static PieceMoveSet[] QueenMoves1;
        internal static PieceMoveSet[] QueenMoves2;
        internal static PieceMoveSet[] QueenMoves3;
        internal static PieceMoveSet[] QueenMoves4;
        internal static PieceMoveSet[] QueenMoves5;
        internal static PieceMoveSet[] QueenMoves6;
        internal static PieceMoveSet[] QueenMoves7;
        internal static PieceMoveSet[] QueenMoves8;

        internal static PieceMoveSet[] RookMoves1;
        internal static PieceMoveSet[] RookMoves2;
        internal static PieceMoveSet[] RookMoves3;
        internal static PieceMoveSet[] RookMoves4;

        internal static PieceMoveSet[] KingMoves;

        #region StaticConstructor

        static MoveArrays()
        {
            WhitePawnMoves = new PieceMoveSet[64];

            BlackPawnMoves = new PieceMoveSet[64];

            KnightMoves = new PieceMoveSet[64];

            BishopMoves1 = new PieceMoveSet[64];

            BishopMoves2 = new PieceMoveSet[64];

            BishopMoves3 = new PieceMoveSet[64];

            BishopMoves4 = new PieceMoveSet[64];

            RookMoves1 = new PieceMoveSet[64];

            RookMoves2 = new PieceMoveSet[64];

            RookMoves3 = new PieceMoveSet[64];

            RookMoves4 = new PieceMoveSet[64];

            QueenMoves1 = new PieceMoveSet[64];

            QueenMoves2 = new PieceMoveSet[64];

            QueenMoves3 = new PieceMoveSet[64];

            QueenMoves4 = new PieceMoveSet[64];

            QueenMoves5 = new PieceMoveSet[64];

            QueenMoves6 = new PieceMoveSet[64];

            QueenMoves7 = new PieceMoveSet[64];

            QueenMoves8 = new PieceMoveSet[64];

            KingMoves = new PieceMoveSet[64];

            SetMovesWhitePawn();
            SetMovesBlackPawn();
            SetMovesKnight();
            SetMovesBishop();
            SetMovesRook();
            SetMovesQueen();
            SetMovesKing();
        }

        #endregion

        #region PrivateHelperMethods

        private static byte Position(byte col, byte row)
        {
            return (byte)(col + (row * 8));
        }

        private static void SetMovesBlackPawn()
        {
            for (byte index = 8; index <= 55; index++)
            {
                var moveset = new PieceMoveSet(new List<byte>());

                byte x = (byte)(index % 8);
                byte y = (byte)((index / 8));

                //Diagonal Kill
                if (y < 7 && x < 7)
                {
                    moveset.Moves.Add((byte)(index + 8 + 1));
                }
                if (x > 0 && y < 7)
                {
                    moveset.Moves.Add((byte)(index + 8 - 1));
                }

                //One Forward
                moveset.Moves.Add((byte)(index + 8));

                //Starting Position we can jump 2
                if (y == 1)
                {
                    moveset.Moves.Add((byte)(index + 16));
                }

                BlackPawnMoves[index] = moveset;
            }
        }

        private static void SetMovesWhitePawn()
        {
            for (byte index = 8; index <= 55; index++)
            {
                byte x = (byte)(index % 8);
                byte y = (byte)((index / 8));

                var moveset = new PieceMoveSet(new List<byte>());

                //Diagonal Kill
                if (x < 7 && y > 0)
                {
                    moveset.Moves.Add((byte)(index - 8 + 1));
                }
                if (x > 0 && y > 0)
                {
                    moveset.Moves.Add((byte)(index - 8 - 1));
                }

                //One Forward
                moveset.Moves.Add((byte)(index - 8));

                //Starting Position we can jump 2
                if (y == 6)
                {
                    moveset.Moves.Add((byte)(index - 16));
                }

                WhitePawnMoves[index] = moveset;
            }
        }

        private static void SetMovesKnight()
        {
            for (byte y = 0; y < 8; y++)
            {
                for (byte x = 0; x < 8; x++)
                {
                    byte index = (byte)(y + (x * 8));

                    var moveset = new PieceMoveSet(new List<byte>());

                    byte move;

                    if (y < 6 && x > 0)
                    {
                        move = Position((byte)(y + 2), (byte)(x - 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y > 1 && x < 7)
                    {
                        move = Position((byte)(y - 2), (byte)(x + 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y > 1 && x > 0)
                    {
                        move = Position((byte)(y - 2), (byte)(x - 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y < 6 && x < 7)
                    {
                        move = Position((byte)(y + 2), (byte)(x + 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y > 0 && x < 6)
                    {
                        move = Position((byte)(y - 1), (byte)(x + 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y < 7 && x > 1)
                    {
                        move = Position((byte)(y + 1), (byte)(x - 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y > 0 && x > 1)
                    {
                        move = Position((byte)(y - 1), (byte)(x - 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    if (y < 7 && x < 6)
                    {
                        move = Position((byte)(y + 1), (byte)(x + 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                        }
                    }

                    KnightMoves[index] = moveset;
                }
            }
        }

        private static void SetMovesBishop()
        {
            for (byte y = 0; y < 8; y++)
            {
                for (byte x = 0; x < 8; x++)
                {
                    byte index = (byte)(y + (x * 8));

                    var moveset = new PieceMoveSet(new List<byte>());
                    byte move;

                    byte row = x;
                    byte col = y;

                    while (row < 7 && col < 7)
                    {
                        row++;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    BishopMoves1[index] = moveset;
                    moveset = new PieceMoveSet(new List<byte>());

                    row = x;
                    col = y;

                    while (row < 7 && col > 0)
                    {
                        row++;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    BishopMoves2[index] = moveset;
                    moveset = new PieceMoveSet(new List<byte>());

                    row = x;
                    col = y;

                    while (row > 0 && col < 7)
                    {
                        row--;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    BishopMoves3[index] = moveset;
                    moveset = new PieceMoveSet(new List<byte>());

                    row = x;
                    col = y;

                    while (row > 0 && col > 0)
                    {
                        row--;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    BishopMoves4[index] = moveset;
                }
            }
        }

        private static void SetMovesRook()
        {
            for (byte y = 0; y < 8; y++)
            {
                for (byte x = 0; x < 8; x++)
                {
                    byte index = (byte)(y + (x * 8));

                    var moveset = new PieceMoveSet(new List<byte>());
                    byte move;

                    byte row = x;
                    byte col = y;

                    while (row < 7)
                    {
                        row++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    RookMoves1[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row > 0)
                    {
                        row--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    RookMoves2[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (col > 0)
                    {
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    RookMoves3[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (col < 7)
                    {
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    RookMoves4[index] = moveset;
                }
            }
        }

        private static void SetMovesQueen()
        {
            for (byte y = 0; y < 8; y++)
            {
                for (byte x = 0; x < 8; x++)
                {
                    byte index = (byte)(y + (x * 8));

                    var moveset = new PieceMoveSet(new List<byte>());
                    byte move;

                    byte row = x;
                    byte col = y;

                    while (row < 7)
                    {
                        row++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves1[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row > 0)
                    {
                        row--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves2[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (col > 0)
                    {
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves3[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (col < 7)
                    {
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves4[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row < 7 && col < 7)
                    {
                        row++;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves5[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row < 7 && col > 0)
                    {
                        row++;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves6[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row > 0 && col < 7)
                    {
                        row--;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves7[index] = moveset;

                    moveset = new PieceMoveSet(new List<byte>());
                    row = x;
                    col = y;

                    while (row > 0 && col > 0)
                    {
                        row--;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    QueenMoves8[index] = moveset;
                }
            }
        }

        private static void SetMovesKing()
        {
            for (byte y = 0; y < 8; y++)
            {
                for (byte x = 0; x < 8; x++)
                {
                    byte index = (byte)(y + (x * 8));

                    var moveset = new PieceMoveSet(new List<byte>());
                    byte move;

                    byte row = x;
                    byte col = y;

                    if (row < 7)
                    {
                        row++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (row > 0)
                    {
                        row--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (col > 0)
                    {
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (col < 7)
                    {
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (row < 7 && col < 7)
                    {
                        row++;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (row < 7 && col > 0)
                    {
                        row++;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    row = x;
                    col = y;

                    if (row > 0 && col < 7)
                    {
                        row--;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }


                    row = x;
                    col = y;

                    if (row > 0 && col > 0)
                    {
                        row--;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                    }

                    KingMoves[index] = moveset;
                }
            }
        }

        #endregion
    }
}