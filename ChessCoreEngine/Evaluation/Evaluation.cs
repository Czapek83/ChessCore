
using System;

namespace ChessEngine.Engine
{
    internal static class Evaluation
    {
        public static int EvaluateBoardScore(EvaluationParameters evaluationParameters, Board board)
        {
            //Black Score - 
            //White Score +
            int result = 0;

            if (evaluationParameters.IsDraw 
                || evaluationParameters.FiftyMove >= 50
                || evaluationParameters.RepeatedMove >= 3)
            {
                return 0;
            }

            if (evaluationParameters.BlackMate)
            {
                return 32767;
            }
            if (evaluationParameters.WhiteMate)
            {
                return -32767;
            }
            if (evaluationParameters.BlackCheck)
            {
                result += 70;
                if (evaluationParameters.EndGamePhase)
                    result += 10;
            }
            else if (evaluationParameters.WhiteCheck)
            {
                result -= 70;
                if (evaluationParameters.EndGamePhase)
                    result -= 10;
            }
            if (evaluationParameters.BlackCastled)
            {
                result -= 50;
            }
            if (evaluationParameters.WhiteCastled)
            {
                result += 50;
            }
            //Add a small bonus for tempo (turn)
            if (evaluationParameters.WhoseMove == ChessPieceColor.White)
            {
                result += 10;
            }
            else
            {
                result -= 10;
            }

            var blackPawnCount = new short[8];
            var whitePawnCount = new short[8];

            for (byte x = 0; x < 64; x++)
            {
                Square square = board.Squares[x];

                if (square.Piece == null)
                    continue;

                var pieceEvaluation = square.Piece.EvaluatePieceScore(x, evaluationParameters.EndGamePhase, whitePawnCount, blackPawnCount);

                result = square.Piece.PieceColor == ChessPieceColor.White ?
                    result + pieceEvaluation :
                    result - pieceEvaluation;

                result += CheckWhiteKingSafety(board);
                result -= CheckBlackKingSafety(board);
            }

            if (evaluationParameters.InsufficientMaterial)
            {
                return 0;
            }

            if (evaluationParameters.EndGamePhase)
            {
                if (evaluationParameters.BlackCheck)
                {
                    result += 10;
                }
                else if (evaluationParameters.WhiteCheck)
                {
                    result -= 10;
                }
            }
            else
            {
                if (!evaluationParameters.WhiteCanCastle && !evaluationParameters.WhiteCastled)
                {
                    result -= 50;
                }
                if (!evaluationParameters.BlackCanCastle && !evaluationParameters.BlackCastled)
                {
                    result += 50;
                }
            }

            //Black Isolated Pawns
            result += CheckIsolatedPawns(blackPawnCount);

            //White Isolated Pawns
            result -= CheckIsolatedPawns(whitePawnCount);

            //Black Passed Pawns
            result -= CheckPassedPawns(blackPawnCount, whitePawnCount);

            //White Passed Pawns
            result += CheckPassedPawns(whitePawnCount, blackPawnCount);

            return result;
        }

        private static int CheckBlackKingSafety(Board board)
        {
            var result = 0;

            var blackKingPosition = board.BlackKingPosition;

            if (blackKingPosition != 3 && blackKingPosition != 4)
            {
                int pawnPos = blackKingPosition + 8;

                result -= CheckPawnWall(board, pawnPos, blackKingPosition);

                pawnPos = blackKingPosition + 7;

                result -= CheckPawnWall(board, pawnPos, blackKingPosition);

                pawnPos = blackKingPosition + 9;

                result -= CheckPawnWall(board, pawnPos, blackKingPosition);
            }
            return result;
        }

        private static int CheckWhiteKingSafety(Board board)
        {
            var result = 0;

            byte whiteKingPosition = board.WhiteKingPosition;
            if (whiteKingPosition != 59 && whiteKingPosition != 60)
            {
                int pawnPos = whiteKingPosition - 8;

                result += CheckPawnWall(board, pawnPos, whiteKingPosition);

                pawnPos = whiteKingPosition - 7;

                result += CheckPawnWall(board, pawnPos, whiteKingPosition);

                pawnPos = whiteKingPosition - 9;

                result += CheckPawnWall(board, pawnPos, whiteKingPosition);
            }

            return result;
        }

        private static int CheckPawnWall(Board board, int pawnPos, int kingPos)
        {

            if (kingPos % 8 == 7 && pawnPos % 8 == 0)
            {
                return 0;
            }

            if (kingPos % 8 == 0 && pawnPos % 8 == 7)
            {
                return 0;
            }

            if (pawnPos > 63 || pawnPos < 0)
            {
                return 0;
            }

            if (board.Squares[pawnPos].Piece != null)
            {
                if (board.Squares[pawnPos].Piece.PieceColor == board.Squares[kingPos].Piece.PieceColor)
                {
                    if (board.Squares[pawnPos].Piece.PieceType == ChessPieceType.Pawn)
                    {
                        return 10;
                    }
                }
            }

            return 0;
        }

        private static int CheckIsolatedPawns(short[] pawnCount)
        {
            var result = 0;
            if (pawnCount[0] >= 1 && pawnCount[1] == 0)
            {
                result += 12;
            }
            if (pawnCount[1] >= 1 && pawnCount[0] == 0 &&
                pawnCount[2] == 0)
            {
                result += 14;
            }
            if (pawnCount[2] >= 1 && pawnCount[1] == 0 &&
                pawnCount[3] == 0)
            {
                result += 16;
            }
            if (pawnCount[3] >= 1 && pawnCount[2] == 0 &&
                pawnCount[4] == 0)
            {
                result += 20;
            }
            if (pawnCount[4] >= 1 && pawnCount[3] == 0 &&
                pawnCount[5] == 0)
            {
                result += 20;
            }
            if (pawnCount[5] >= 1 && pawnCount[4] == 0 &&
                pawnCount[6] == 0)
            {
                result += 16;
            }
            if (pawnCount[6] >= 1 && pawnCount[5] == 0 &&
                pawnCount[7] == 0)
            {
                result += 14;
            }
            if (pawnCount[7] >= 1 && pawnCount[6] == 0)
            {
                result += 12;
            }

            return result;
        }

        private static int CheckPassedPawns(short[] myPawnCount, short[] oppositePawnCount)
        {
            var result = 0;

            if (myPawnCount[0] >= 1 && oppositePawnCount[0] == 0)
            {
                result -= myPawnCount[0];
            }
            if (myPawnCount[1] >= 1 && oppositePawnCount[1] == 0)
            {
                result -= myPawnCount[1];
            }
            if (myPawnCount[2] >= 1 && oppositePawnCount[2] == 0)
            {
                result -= myPawnCount[2];
            }
            if (myPawnCount[3] >= 1 && oppositePawnCount[3] == 0)
            {
                result -= myPawnCount[3];
            }
            if (myPawnCount[4] >= 1 && oppositePawnCount[4] == 0)
            {
                result -= myPawnCount[4];
            }
            if (myPawnCount[5] >= 1 && oppositePawnCount[5] == 0)
            {
                result -= myPawnCount[5];
            }
            if (myPawnCount[6] >= 1 && oppositePawnCount[6] == 0)
            {
                result -= myPawnCount[6];
            }
            if (myPawnCount[7] >= 1 && oppositePawnCount[7] == 0)
            {
                result -= myPawnCount[7];
            }

            return result;
        }
    }
}