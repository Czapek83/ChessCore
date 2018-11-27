using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class Queen : Piece
    {
        public Queen(ChessPieceColor color) : base(ChessPieceType.Queen, color)
        {

        }

        public override short PieceValue => 975;
        public override short PieceActionValue => 1;

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, 
            byte index, short[] whitePawnTable, short[] blackPawnTable)
        {
            var score = 0;
            if (Moved && !endGamePhase)
            {
                score -= 10;
            }
            return score;
        }

        public override string GetPieceTypeShort()
        {
            return "Q";
        }

        public void GenerateMoves(byte piecePosition, Board board)
        {
            for (byte i = 0; i < MoveArrays.QueenMoves1[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves1[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves2[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves2[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves3[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves3[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves4[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves4[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }

            for (byte i = 0; i < MoveArrays.QueenMoves5[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves5[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves6[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves6[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves7[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves7[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.QueenMoves8[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.QueenMoves8[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
        }
    }
}
