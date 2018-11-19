
namespace ChessEngine.Engine
{
    internal static class Evaluation
    {
        public static short[] blackPawnCount;
        public static short[] whitePawnCount;
      
        public static readonly short[] PawnTable = new short[]
        {
       	     0,  0,  0,  0,  0,  0,  0,  0,
            50, 50, 50, 50, 50, 50, 50, 50,
            20, 20, 30, 40, 40, 30, 20, 20,
             5,  5, 10, 30, 30, 10,  5,  5,
             0,  0,  0, 25, 25,  0,  0,  0,
             5, -5,-10,  0,  0,-10, -5,  5,
             5, 10, 10,-30,-30, 10, 10,  5,
             0,  0,  0,  0,  0,  0,  0,  0
        };

        public static readonly short[] KnightTable = new short[]
        {
            -50,-40,-30,-30,-30,-30,-40,-50,
            -40,-20,  0,  0,  0,  0,-20,-40,
            -30,  0, 10, 15, 15, 10,  0,-30,
            -30,  5, 15, 20, 20, 15,  5,-30,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -30,  5, 10, 15, 15, 10,  5,-30,
            -40,-20,  0,  5,  5,  0,-20,-40,
            -50,-30,-20,-30,-30,-20,-30,-50,
        };

        public static readonly short[] BishopTable = new short[]
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5, 10, 10,  5,  0,-10,
            -10,  5,  5, 10, 10,  5,  5,-10,
            -10,  0, 10, 10, 10, 10,  0,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  5,  0,  0,  0,  0,  5,-10,
            -20,-10,-40,-10,-10,-40,-10,-20,
        };

        public static readonly short[] KingTable = new short[]
        {
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -30, -40, -40, -50, -50, -40, -40, -30,
          -20, -30, -30, -40, -40, -30, -30, -20,
          -10, -20, -20, -20, -20, -20, -20, -10, 
           20,  20,   0,   0,   0,   0,  20,  20,
           20,  30,  10,   0,   0,  10,  30,  20
        };

        public static readonly short[] KingTableEndGame = new short[]
        {
            -50,-40,-30,-20,-20,-30,-40,-50,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-30,  0,  0,  0,  0,-30,-30,
            -50,-30,-30,-30,-30,-30,-30,-50
        };

        public static int EvaluatePieceScore(Piece piece, byte position, bool endGamePhase)
        {
            int score = 0;

            byte index = position;

            if (piece.PieceColor == ChessPieceColor.Black)
            {
                index = (byte)(63-position);
            }

            //Calculate Piece Values
            score += piece.PieceValue;
            score += piece.DefendedValue;
            score -= piece.AttackedValue;

            //Double Penalty for Hanging Pieces
            if (piece.DefendedValue < piece.AttackedValue)
            {
                score -= ((piece.AttackedValue - piece.DefendedValue)* 10);
            }

            //Add Points for Mobility
            if (piece.ValidMoves != null)
            {
                score += piece.ValidMoves.Count;
            }

            if (piece.PieceType == ChessPieceType.Pawn)
            {
                if (position % 8 == 0 || position % 8 == 7)
                {
                    //Rook Pawns are worth 15% less because they can only attack one way
                    score -= 15;
                }

                //Calculate Position Values
                score += PawnTable[index];

                if (piece.PieceColor == ChessPieceColor.White)
                {
                    if (whitePawnCount[position % 8] > 0)
                    {
                        //Doubled Pawn
                        score -= 15;
                    }

                    if (position >= 8 && position <= 15)
                    {
                        if (piece.AttackedValue == 0)
                        {
                            whitePawnCount[position % 8] += 50;

                            if (piece.DefendedValue != 0)
                                whitePawnCount[position % 8] += 50;
                        }
                    }
                    else if (position >= 16 && position <= 23)
                    {
                        if (piece.AttackedValue == 0)
                        {
                            whitePawnCount[position % 8] += 25;


                            if (piece.DefendedValue != 0)
                                whitePawnCount[position % 8] += 25;
                        }
                    }

                    whitePawnCount[position % 8]+=10;
                }
                else
                {
                    if (blackPawnCount[position % 8] > 0)
                    {
                        //Doubled Pawn
                        score -= 15;
                    }

                    if (position >= 48 && position <= 55)
                    {
                        if (piece.AttackedValue == 0)
                        {
                            blackPawnCount[position % 8] += 50;

                            if (piece.DefendedValue != 0)
                                blackPawnCount[position % 8] += 50;
                        }
                    }
                    //Pawns in 6th Row that are not attacked are worth more points.
                    else if (position >= 40 && position <= 47)
                    {
                        if (piece.AttackedValue == 0)
                        {
                            blackPawnCount[position % 8] += 25;

                            if (piece.DefendedValue != 0)
                                blackPawnCount[position % 8] += 25;
                        }
                    }

                    blackPawnCount[position % 8] += 10;
                    
                }
            }
            else if (piece.PieceType == ChessPieceType.Knight)
            {
                score += KnightTable[index];

                //In the end game remove a few points for Knights since they are worth less
                if (endGamePhase)
                {
                    score -= 10;
                }

            }
            else if (piece.PieceType == ChessPieceType.Bishop)
            {
                //In the end game Bishops are worth more
                if (endGamePhase)
                {
                    score += 10;
                }

                score += BishopTable[index];
            }
            else if (piece.PieceType == ChessPieceType.Queen)
            {
                if (piece.Moved && !endGamePhase)
                {
                    score -= 10;
                }
            }
            else if (piece.PieceType == ChessPieceType.King)
            {
                if (piece.ValidMoves != null)
                {
                    if (piece.ValidMoves.Count < 2)
                    {
                        score -= 5;
                    }
                }

                if (endGamePhase)
                {
                    score += KingTableEndGame[index];
                }
                else
                {
                    score += KingTable[index];
                }
            }

            return score;
        }

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
                    result += EvaluatePieceScore(square.Piece, x, evaluationParameters.EndGamePhase);

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
                    result -= EvaluatePieceScore(square.Piece, x, evaluationParameters.EndGamePhase);


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