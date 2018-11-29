using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessEngine.Engine
{
    public class Rook : Piece
    {
        public Rook(ChessColor color, ICoordinatesConverter coordinatesConverter) : base(ChessPieceType.Rook, color, coordinatesConverter)
        {

        }

        public override short PieceValue => 500;
        public override short PieceActionValue => 2;

        public override int EvaluatePieceSpecificScore(byte position, bool endGamePhase, 
            byte index, PawnCount _)
        {
            return 0;
        }

        public override string GetPieceTypeShort()
        {
            return "R";
        }

        public override void GenerateMoves(byte piecePosition, Board board)
        {
            if (Moved)
            {
                var rooksMoveCount = board.Squares.Where(x => x.Piece != null
                    && x.Piece.PieceType == ChessPieceType.Rook && x.Piece.PieceColor == PieceColor && x.Piece.Moved).Count();

                if (rooksMoveCount > 1)
                {
                    board.SetCantCastle(PieceColor);
                }
            }


            for (byte i = 0; i < MoveArrays.RookMoves1[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.RookMoves1[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.RookMoves2[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.RookMoves2[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.RookMoves3[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.RookMoves3[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
            for (byte i = 0; i < MoveArrays.RookMoves4[piecePosition].Moves.Count; i++)
            {
                if (
                    AnalyzeMove(MoveArrays.RookMoves4[piecePosition].Moves[i], board) ==
                    false)
                {
                    break;
                }
            }
        }
    }
}
