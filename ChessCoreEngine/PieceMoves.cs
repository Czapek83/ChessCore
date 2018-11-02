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
        internal static byte[] BishopTotalMoves1;
        
        internal static PieceMoveSet[] BishopMoves2;
        internal static byte[] BishopTotalMoves2;

        internal static PieceMoveSet[] BishopMoves3;
        internal static byte[] BishopTotalMoves3;

        internal static PieceMoveSet[] BishopMoves4;
        internal static byte[] BishopTotalMoves4;

        internal static PieceMoveSet[] BlackPawnMoves;
        internal static byte[] BlackPawnTotalMoves;

        internal static PieceMoveSet[] WhitePawnMoves;
        internal static byte[] WhitePawnTotalMoves;

        internal static PieceMoveSet[] KnightMoves;
        internal static byte[] KnightTotalMoves;

        internal static PieceMoveSet[] QueenMoves1;
        internal static byte[] QueenTotalMoves1;
        internal static PieceMoveSet[] QueenMoves2;
        internal static byte[] QueenTotalMoves2;
        internal static PieceMoveSet[] QueenMoves3;
        internal static byte[] QueenTotalMoves3;
        internal static PieceMoveSet[] QueenMoves4;
        internal static byte[] QueenTotalMoves4;
        internal static PieceMoveSet[] QueenMoves5;
        internal static byte[] QueenTotalMoves5;
        internal static PieceMoveSet[] QueenMoves6;
        internal static byte[] QueenTotalMoves6;
        internal static PieceMoveSet[] QueenMoves7;
        internal static byte[] QueenTotalMoves7;
        internal static PieceMoveSet[] QueenMoves8;
        internal static byte[] QueenTotalMoves8;

        internal static PieceMoveSet[] RookMoves1;
        internal static byte[] RookTotalMoves1;
        internal static PieceMoveSet[] RookMoves2;
        internal static byte[] RookTotalMoves2;
        internal static PieceMoveSet[] RookMoves3;
        internal static byte[] RookTotalMoves3;
        internal static PieceMoveSet[] RookMoves4;
        internal static byte[] RookTotalMoves4;

        internal static PieceMoveSet[] KingMoves;
        internal static byte[] KingTotalMoves;

        #region StaticConstructor

        static MoveArrays()
        {
            WhitePawnMoves = new PieceMoveSet[64];
            WhitePawnTotalMoves = new byte[64];

            BlackPawnMoves = new PieceMoveSet[64];
            BlackPawnTotalMoves = new byte[64];

            KnightMoves = new PieceMoveSet[64];
            KnightTotalMoves = new byte[64];

            BishopMoves1 = new PieceMoveSet[64];
            BishopTotalMoves1 = new byte[64];

            BishopMoves2 = new PieceMoveSet[64];
            BishopTotalMoves2 = new byte[64];

            BishopMoves3 = new PieceMoveSet[64];
            BishopTotalMoves3 = new byte[64];

            BishopMoves4 = new PieceMoveSet[64];
            BishopTotalMoves4 = new byte[64];

            RookMoves1 = new PieceMoveSet[64];
            RookTotalMoves1 = new byte[64];

            RookMoves2 = new PieceMoveSet[64];
            RookTotalMoves2 = new byte[64];

            RookMoves3 = new PieceMoveSet[64];
            RookTotalMoves3 = new byte[64];

            RookMoves4 = new PieceMoveSet[64];
            RookTotalMoves4 = new byte[64];

            QueenMoves1 = new PieceMoveSet[64];
            QueenTotalMoves1 = new byte[64];

            QueenMoves2 = new PieceMoveSet[64];
            QueenTotalMoves2 = new byte[64];

            QueenMoves3 = new PieceMoveSet[64];
            QueenTotalMoves3 = new byte[64];

            QueenMoves4 = new PieceMoveSet[64];
            QueenTotalMoves4 = new byte[64];

            QueenMoves5 = new PieceMoveSet[64];
            QueenTotalMoves5 = new byte[64];

            QueenMoves6 = new PieceMoveSet[64];
            QueenTotalMoves6 = new byte[64];

            QueenMoves7 = new PieceMoveSet[64];
            QueenTotalMoves7 = new byte[64];

            QueenMoves8 = new PieceMoveSet[64];
            QueenTotalMoves8 = new byte[64];

            KingMoves = new PieceMoveSet[64];
            KingTotalMoves = new byte[64];

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
                    BlackPawnTotalMoves[index]++;
                }
                if (x > 0 && y < 7)
                {
                    moveset.Moves.Add((byte)(index + 8 - 1));
                    BlackPawnTotalMoves[index]++;
                }

                //One Forward
                moveset.Moves.Add((byte)(index + 8));
                BlackPawnTotalMoves[index]++;

                //Starting Position we can jump 2
                if (y == 1)
                {
                    moveset.Moves.Add((byte)(index + 16));
                    BlackPawnTotalMoves[index]++;
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
                    WhitePawnTotalMoves[index]++;
                }
                if (x > 0 && y > 0)
                {
                    moveset.Moves.Add((byte)(index - 8 - 1));
                    WhitePawnTotalMoves[index]++;
                }

                //One Forward
                moveset.Moves.Add((byte)(index - 8));
                WhitePawnTotalMoves[index]++;

                //Starting Position we can jump 2
                if (y == 6)
                {
                    moveset.Moves.Add((byte)(index - 16));
                    WhitePawnTotalMoves[index]++;
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
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y > 1 && x < 7)
                    {
                        move = Position((byte)(y - 2), (byte)(x + 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y > 1 && x > 0)
                    {
                        move = Position((byte)(y - 2), (byte)(x - 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y < 6 && x < 7)
                    {
                        move = Position((byte)(y + 2), (byte)(x + 1));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y > 0 && x < 6)
                    {
                        move = Position((byte)(y - 1), (byte)(x + 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y < 7 && x > 1)
                    {
                        move = Position((byte)(y + 1), (byte)(x - 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y > 0 && x > 1)
                    {
                        move = Position((byte)(y - 1), (byte)(x - 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
                        }
                    }

                    if (y < 7 && x < 6)
                    {
                        move = Position((byte)(y + 1), (byte)(x + 2));

                        if (move < 64)
                        {
                            moveset.Moves.Add(move);
                            KnightTotalMoves[index]++;
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
                        BishopTotalMoves1[index]++;
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
                        BishopTotalMoves2[index]++;
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
                        BishopTotalMoves3[index]++;
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
                        BishopTotalMoves4[index]++;
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
                        RookTotalMoves1[index]++;
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
                        RookTotalMoves2[index]++;
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
                        RookTotalMoves3[index]++;
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
                        RookTotalMoves4[index]++;
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
                        QueenTotalMoves1[index]++;
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
                        QueenTotalMoves2[index]++;
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
                        QueenTotalMoves3[index]++;
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
                        QueenTotalMoves4[index]++;
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
                        QueenTotalMoves5[index]++;
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
                        QueenTotalMoves6[index]++;
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
                        QueenTotalMoves7[index]++;
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
                        QueenTotalMoves8[index]++;
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
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (row > 0)
                    {
                        row--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (col > 0)
                    {
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (col < 7)
                    {
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (row < 7 && col < 7)
                    {
                        row++;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (row < 7 && col > 0)
                    {
                        row++;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    row = x;
                    col = y;

                    if (row > 0 && col < 7)
                    {
                        row--;
                        col++;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }


                    row = x;
                    col = y;

                    if (row > 0 && col > 0)
                    {
                        row--;
                        col--;

                        move = Position(col, row);
                        moveset.Moves.Add(move);
                        KingTotalMoves[index]++;
                    }

                    KingMoves[index] = moveset;
                }
            }
        }

        #endregion
    }
}