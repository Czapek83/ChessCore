using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Loggers;
using ChessEngine.Engine.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class NewGameBoardFactory : BoardFactory
    {
        public NewGameBoardFactory(LoggerBase loggerBase):base(loggerBase)
        {

        }

        public override Board CreateBoard()
        {
            _logger.LogInfo($"BoardFactory.CreateNewGameBoard() start");
            WhoseMove = ChessPieceColor.White;

            Squares[0].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.Black);
            Squares[1].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.Black);
            Squares[2].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.Black);
            Squares[3].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Queen, ChessPieceColor.Black);
            Squares[4].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.King, ChessPieceColor.Black);
            Squares[5].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.Black);
            Squares[6].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.Black);
            Squares[7].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.Black);

            for (int i = 8; i < 16; i++)
                Squares[i].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 8; i < 16; i++)
                Squares[i].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Pawn, ChessPieceColor.Black);

            for (int i = 48; i < 56; i++)
                Squares[i].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Pawn, ChessPieceColor.White);

            Squares[56].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.White);
            Squares[57].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.White);
            Squares[58].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.White);
            Squares[59].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Queen, ChessPieceColor.White);
            Squares[60].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.King, ChessPieceColor.White);
            Squares[61].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.White);
            Squares[62].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.White);
            Squares[63].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.White);

            GenerateValidMoves();
            _logger.LogDebug($"BoardFactory.CreateNewGameBoard() end");
            return this;
        }

    }
}
