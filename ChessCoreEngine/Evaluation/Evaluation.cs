
using System;
using System.Collections.Generic;

namespace ChessEngine.Engine
{
    internal static class Evaluation
    {
        public static int EvaluateBoardScore(EvaluationParameters evaluationParameters, Board board)
        {
            //Black Score - 
            //White Score +
            int result = 0;

            board.GenerateValidMoves();

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

            var pawnCount = new PawnCountDictionary();

            for (byte x = 0; x < 64; x++)
            {
                Square square = board.Squares[x];

                if (square.Piece == null)
                    continue;

                var pieceEvaluation = square.Piece.EvaluatePieceScore(x, evaluationParameters.EndGamePhase, pawnCount);

                result = square.Piece.PieceColor == ChessPieceColor.White ?
                    result + pieceEvaluation :
                    result - pieceEvaluation;
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

            result += CheckWhiteKingSafety(board);
            result += CheckBlackKingSafety(board);

            //Black Isolated Pawns
            result += CheckIsolatedPawns(pawnCount[ChessPieceColor.Black]);

            //White Isolated Pawns
            result -= CheckIsolatedPawns(pawnCount[ChessPieceColor.White]);

            //Black Passed Pawns
            result -= CheckPassedPawns(ChessPieceColor.Black, pawnCount);

            //White Passed Pawns
            result += CheckPassedPawns(ChessPieceColor.White, pawnCount);

            return result;
        }

        private static int CheckBlackKingSafety(Board board)
        {
            var result = 0;

            var blackKingPosition = board.GetKingPosition(ChessPieceColor.Black);

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

            byte whiteKingPosition = board.GetKingPosition(ChessPieceColor.White);
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

        private static int CheckPassedPawns(ChessColor myChessColor, PawnCountDictionary pawnCount)
        {
            var result = 0;

            if (pawnCount[myChessColor][0] >= 1 && pawnCount[!myChessColor][0] == 0)
            {
                result += pawnCount[myChessColor][0];
            }
            if (pawnCount[myChessColor][1] >= 1 && pawnCount[!myChessColor][1] == 0)
            {
                result += pawnCount[myChessColor][1];
            }
            if (pawnCount[myChessColor][2] >= 1 && pawnCount[!myChessColor][2] == 0)
            {
                result += pawnCount[myChessColor][2];
            }
            if (pawnCount[myChessColor][3] >= 1 && pawnCount[!myChessColor][3] == 0)
            {
                result += pawnCount[myChessColor][3];
            }
            if (pawnCount[myChessColor][4] >= 1 && pawnCount[!myChessColor][4] == 0)
            {
                result += pawnCount[myChessColor][4];
            }
            if (pawnCount[myChessColor][5] >= 1 && pawnCount[!myChessColor][5] == 0)
            {
                result += pawnCount[myChessColor][5];
            }
            if (pawnCount[myChessColor][6] >= 1 && pawnCount[!myChessColor][6] == 0)
            {
                result += pawnCount[myChessColor][6];
            }
            if (pawnCount[myChessColor][7] >= 1 && pawnCount[!myChessColor][7] == 0)
            {
                result += pawnCount[myChessColor][7];
            }

            return result;
        }
    }
}