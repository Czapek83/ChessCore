using ChessEngine.Engine.Pieces;
using System;


namespace ChessEngine.Engine
{
    public static class Puzzle
    {
        public static Engine NewPuzzleKnightBishopKing()
        {
            Engine engine;

            do
            {
                engine = PuzzleKnightBishopCandidate();
            }
            while (engine.IsGameOver() || engine.GetChecked(ChessPieceColor.Black) || engine.GetChecked(ChessPieceColor.White));
            return engine;
        }

        public static Engine NewPuzzleRookKing()
        {
            Engine engine;

            do
            {
                engine = PuzzleRookCandidate();
            }
            while (engine.IsGameOver() || engine.GetChecked(ChessPieceColor.Black) || engine.GetChecked(ChessPieceColor.White));
            return engine;
        }

        public static Engine NewPuzzlePawnKing()
        {
            Engine engine;

            do
            {
                engine = PuzzleKingPawnCandidate();
            }
            while (engine.IsGameOver() || engine.GetChecked(ChessPieceColor.Black) || engine.GetChecked(ChessPieceColor.White));
            return engine;
        }

        private static Engine PuzzleKnightBishopCandidate()
        {
            Engine engine = new Engine(Board.CreateEmptyBoard());

            Random random = new Random(DateTime.Now.Second);

            byte whiteKingIndex;
            byte blackKingIndex;
            byte whiteKnightIndex;
            byte whiteBishopIndex;

            do
            {
                whiteKingIndex = (byte)random.Next(63);
                blackKingIndex = (byte)random.Next(63);
                whiteKnightIndex = (byte)random.Next(63);
                whiteBishopIndex = (byte)random.Next(63);
            }
            while (
                whiteKingIndex == blackKingIndex ||
                whiteKingIndex == whiteBishopIndex ||
                whiteKingIndex == whiteKnightIndex ||
                whiteKnightIndex == whiteBishopIndex ||
                blackKingIndex == whiteBishopIndex ||
                blackKingIndex == whiteKingIndex
            );

            var converter = new CoordinatesConverter();

            Piece whiteKing = new King(ChessPieceColor.White, converter);
            Piece whiteBishop = new Bishop(ChessPieceColor.White, converter);
            Piece whiteKnight = new Knight(ChessPieceColor.White, converter);
            Piece blackKing = new King(ChessPieceColor.Black, converter);

            engine.SetChessPiece(whiteKing, whiteKingIndex);
            engine.SetChessPiece(blackKing, blackKingIndex);
            engine.SetChessPiece(whiteKnight, whiteKnightIndex);
            engine.SetChessPiece(whiteBishop, whiteBishopIndex);
            
            engine.GenerateValidMoves();
            engine.EvaluateBoardScore();

            return engine;
        }

        private static Engine PuzzleRookCandidate()
        {
            Engine engine = new Engine(Board.CreateEmptyBoard());

            Random random = new Random(DateTime.Now.Second);

            byte whiteKingIndex;
            byte blackKingIndex;
            byte whiteRookIndex;
            
            do
            {
                whiteKingIndex = (byte)random.Next(63);
                blackKingIndex = (byte)random.Next(63);
                whiteRookIndex = (byte)random.Next(63);
            }
            while (
                whiteKingIndex == blackKingIndex ||
                whiteKingIndex == whiteRookIndex ||
                blackKingIndex == whiteKingIndex
            );
            var converter = new CoordinatesConverter();
            Piece whiteKing = new King(ChessPieceColor.White, converter);
            Piece whiteRook = new Rook(ChessPieceColor.White, converter);
            Piece blackKing = new King(ChessPieceColor.Black, converter);

            engine.SetChessPiece(whiteKing, whiteKingIndex);
            engine.SetChessPiece(blackKing, blackKingIndex);
            engine.SetChessPiece(whiteRook, whiteRookIndex);

            engine.GenerateValidMoves();
            engine.EvaluateBoardScore();

            return engine;
        }

        private static Engine PuzzleKingPawnCandidate()
        {
            Engine engine = new Engine(Board.CreateEmptyBoard());

            Random random = new Random(DateTime.Now.Second);

            byte whiteKingIndex;
            byte blackKingIndex;
            byte whitePawnIndex;

            do
            {
                whiteKingIndex = (byte)random.Next(63);
                blackKingIndex = (byte)random.Next(63);
                whitePawnIndex = (byte)random.Next(63);
            }
            while (
                whiteKingIndex == blackKingIndex ||
                whiteKingIndex == whitePawnIndex ||
                blackKingIndex == whiteKingIndex ||
                whitePawnIndex <= 8 ||whitePawnIndex >= 56 ||
                whitePawnIndex < blackKingIndex
            );

            var converter = new CoordinatesConverter();
            Piece whiteKing = new King(ChessPieceColor.White, converter);
            Piece whitePawn = new Pawn(ChessPieceColor.White, converter);
            Piece blackKing = new King(ChessPieceColor.Black, converter);

            engine.SetChessPiece(whiteKing, whiteKingIndex);
            engine.SetChessPiece(blackKing, blackKingIndex);
            engine.SetChessPiece(whitePawn, whitePawnIndex);

            engine.GenerateValidMoves();
            engine.EvaluateBoardScore();

            return engine;
        }

    }
}
