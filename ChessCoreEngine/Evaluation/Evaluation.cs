
namespace ChessEngine.Engine
{
    internal static class Evaluation
    {
        public static short[] blackPawnCount;
        public static short[] whitePawnCount;

        public static int EvaluateBoardScore(EvaluationParameters evaluationParameters, Board board)
        {
            //Black Score - 
            //White Score +
            int result = 0;

            if (evaluationParameters.IsDraw)
            {
                return 0;
            }
            if (evaluationParameters.FiftyMove >= 50)
            {
                return 0;
            }
            if (evaluationParameters.RepeatedMove >= 3)
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

            blackPawnCount = new short[8];
            whitePawnCount = new short[8];

            for (byte x = 0; x < 64; x++)
            {
                Square square = board.Squares[x];

                if (square.Piece == null)
                    continue;


                if (square.Piece.PieceColor == ChessPieceColor.White)
                {
                    result += square.Piece.EvaluatePieceScore(x, evaluationParameters.EndGamePhase, whitePawnCount, blackPawnCount);

                    if (square.Piece.PieceType == ChessPieceType.King)
                    {
                        if (x != 59 && x != 60)
                        {
                            int pawnPos = x - 8;

                            result += CheckPawnWall(board, pawnPos, x);

                            pawnPos = x - 7;

                            result += CheckPawnWall(board, pawnPos, x);

                            pawnPos = x - 9;

                            result += CheckPawnWall(board, pawnPos, x);
                        }
                    }
                }
                else if (square.Piece.PieceColor == ChessPieceColor.Black)
                {
                    result -= square.Piece.EvaluatePieceScore(x, evaluationParameters.EndGamePhase, whitePawnCount, blackPawnCount);

                    if (square.Piece.PieceType == ChessPieceType.King)
                    {
                        if (x != 3 && x != 4)
                        {
                            int pawnPos = x + 8;

                            result -= CheckPawnWall(board, pawnPos, x);

                            pawnPos = x + 7;

                            result -= CheckPawnWall(board, pawnPos, x);

                            pawnPos = x + 9;

                            result -= CheckPawnWall(board, pawnPos, x);
                        }

                    }
                   
                }

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
            if (blackPawnCount[0] >= 1 && blackPawnCount[1] == 0)
            {
                result += 12;
            }
            if (blackPawnCount[1] >= 1 && blackPawnCount[0] == 0 &&
                blackPawnCount[2] == 0)
            {
                result += 14;
            }
            if (blackPawnCount[2] >= 1 && blackPawnCount[1] == 0 &&
                blackPawnCount[3] == 0)
            {
                result += 16;
            }
            if (blackPawnCount[3] >= 1 && blackPawnCount[2] == 0 &&
                blackPawnCount[4] == 0)
            {
                result += 20;
            }
            if (blackPawnCount[4] >= 1 && blackPawnCount[3] == 0 &&
                blackPawnCount[5] == 0)
            {
                result += 20;
            }
            if (blackPawnCount[5] >= 1 && blackPawnCount[4] == 0 &&
                blackPawnCount[6] == 0)
            {
                result += 16;
            }
            if (blackPawnCount[6] >= 1 && blackPawnCount[5] == 0 &&
                blackPawnCount[7] == 0)
            {
                result += 14;
            }
            if (blackPawnCount[7] >= 1 && blackPawnCount[6] == 0)
            {
                result += 12;
            }

            //White Isolated Pawns
            if (whitePawnCount[0] >= 1 && whitePawnCount[1] == 0)
            {
                result -= 12;
            }
            if (whitePawnCount[1] >= 1 && whitePawnCount[0] == 0 &&
                whitePawnCount[2] == 0)
            {
                result -= 14;
            }
            if (whitePawnCount[2] >= 1 && whitePawnCount[1] == 0 &&
                whitePawnCount[3] == 0)
            {
                result -= 16;
            }
            if (whitePawnCount[3] >= 1 && whitePawnCount[2] == 0 &&
                whitePawnCount[4] == 0)
            {
                result -= 20;
            }
            if (whitePawnCount[4] >= 1 && whitePawnCount[3] == 0 &&
                whitePawnCount[5] == 0)
            {
                result -= 20;
            }
            if (whitePawnCount[5] >= 1 && whitePawnCount[4] == 0 &&
                whitePawnCount[6] == 0)
            {
                result -= 16;
            }
            if (whitePawnCount[6] >= 1 && whitePawnCount[5] == 0 &&
                whitePawnCount[7] == 0)
            {
                result -= 14;
            }
            if (whitePawnCount[7] >= 1 && whitePawnCount[6] == 0)
            {
                result -= 12;
            }

            //Black Passed Pawns
            if (blackPawnCount[0] >= 1 && whitePawnCount[0] == 0)
            {
                result -= blackPawnCount[0];
            }
            if (blackPawnCount[1] >= 1 && whitePawnCount[1] == 0)
            {
                result -= blackPawnCount[1];
            }
            if (blackPawnCount[2] >= 1 && whitePawnCount[2] == 0)
            {
                result -= blackPawnCount[2];
            }
            if (blackPawnCount[3] >= 1 && whitePawnCount[3] == 0)
            {
                result -= blackPawnCount[3];
            }
            if (blackPawnCount[4] >= 1 && whitePawnCount[4] == 0)
            {
                result -= blackPawnCount[4];
            }
            if (blackPawnCount[5] >= 1 && whitePawnCount[5] == 0)
            {
                result -= blackPawnCount[5];
            }
            if (blackPawnCount[6] >= 1 && whitePawnCount[6] == 0)
            {
                result -= blackPawnCount[6];
            }
            if (blackPawnCount[7] >= 1 && whitePawnCount[7] == 0)
            {
                result -= blackPawnCount[7];
            }

            //White Passed Pawns
            if (whitePawnCount[0] >= 1 && blackPawnCount[1] == 0)
            {
                result += whitePawnCount[0];
            }
            if (whitePawnCount[1] >= 1 && blackPawnCount[1] == 0)
            {
                result += whitePawnCount[1];
            }
            if (whitePawnCount[2] >= 1 && blackPawnCount[2] == 0)
            {
                result += whitePawnCount[2];
            }
            if (whitePawnCount[3] >= 1 && blackPawnCount[3] == 0)
            {
                result += whitePawnCount[3];
            }
            if (whitePawnCount[4] >= 1 && blackPawnCount[4] == 0)
            {
                result += whitePawnCount[4];
            }
            if (whitePawnCount[5] >= 1 && blackPawnCount[5] == 0)
            {
                result += whitePawnCount[5];
            }
            if (whitePawnCount[6] >= 1 && blackPawnCount[6] == 0)
            {
                result += whitePawnCount[6];
            }
            if (whitePawnCount[7] >= 1 && blackPawnCount[7] == 0)
            {
                result += whitePawnCount[7];
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
    }
}