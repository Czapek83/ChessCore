using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class NewGameBoardFactory : IBoardFactory
    {
        public Board CreateBoard()
        {
            var result = new Board();

            result.WhoseMove = ChessPieceColor.White;

            result.Squares = new Square[64];

            result.Squares[0].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);
            result.Squares[1].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
            result.Squares[2].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
            result.Squares[3].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.Black);
            result.Squares[4].Piece = new Piece(ChessPieceType.King, ChessPieceColor.Black);
            result.Squares[5].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
            result.Squares[6].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
            result.Squares[7].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);

            for (int i = 8; i<16; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 8; i < 16; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 48; i < 56; i++)
                result.Squares[i].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);

            result.Squares[56].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);
            result.Squares[57].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
            result.Squares[58].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
            result.Squares[59].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.White);
            result.Squares[60].Piece = new Piece(ChessPieceType.King, ChessPieceColor.White);
            result.Squares[61].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
            result.Squares[62].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
            result.Squares[63].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);

            PieceValidMoves.GenerateValidMoves(result);

            return result;
        }
    }
}
