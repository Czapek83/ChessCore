using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class BoardFactory : Board
    {
        public Board CreateBoardFromFen(string fen)
        {
            byte index = 0;

            WhiteCastled = true;
            BlackCastled = true;
            WhiteCanCastle = false;
            BlackCanCastle = false;

            byte spacers = 0;

            WhoseMove = ChessPieceColor.White;

            if (fen.Contains("a3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 40;
            }
            else if (fen.Contains("b3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 41;
            }
            else if (fen.Contains("c3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 42;
            }
            else if (fen.Contains("d3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 43;
            }
            else if (fen.Contains("e3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 44;
            }
            else if (fen.Contains("f3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 45;
            }
            else if (fen.Contains("g3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 46;
            }
            else if (fen.Contains("h3"))
            {
                EnPassantColor = ChessPieceColor.White;
                EnPassantPosition = 47;
            }


            if (fen.Contains("a6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 16;
            }
            else if (fen.Contains("b6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 17;
            }
            else if (fen.Contains("c6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 18;
            }
            else if (fen.Contains("d6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 19;
            }
            else if (fen.Contains("e6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 20;
            }
            else if (fen.Contains("f6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 21;
            }
            else if (fen.Contains("g6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 22;
            }
            else if (fen.Contains("h6"))
            {
                EnPassantColor = ChessPieceColor.Black;
                EnPassantPosition = 23;
            }

            if (fen.Contains(" w "))
            {
                WhoseMove = ChessPieceColor.White;
            }
            if (fen.Contains(" b "))
            {
                WhoseMove = ChessPieceColor.Black;
            }

            foreach (char c in fen)
            {

                if (index < 64 && spacers == 0)
                {
                    if (c == '1' && index < 63)
                    {
                        index++;
                    }
                    else if (c == '2' && index < 62)
                    {
                        index += 2;
                    }
                    else if (c == '3' && index < 61)
                    {
                        index += 3;
                    }
                    else if (c == '4' && index < 60)
                    {
                        index += 4;
                    }
                    else if (c == '5' && index < 59)
                    {
                        index += 5;
                    }
                    else if (c == '6' && index < 58)
                    {
                        index += 6;
                    }
                    else if (c == '7' && index < 57)
                    {
                        index += 7;
                    }
                    else if (c == '8' && index < 56)
                    {
                        index += 8;
                    }
                    else if (c == 'P')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Pawn, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'N')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'B')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'R')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'Q')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Queen, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'K')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.King, ChessPieceColor.White);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'p')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Pawn, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'n')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Knight, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'b')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Bishop, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'r')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Rook, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'q')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.Queen, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'k')
                    {
                        Squares[index].Piece = PieceFactory.CreatePieceByTypeAndColor(ChessPieceType.King, ChessPieceColor.Black);
                        Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == '/')
                    {
                        continue;
                    }
                    else if (c == ' ')
                    {
                        spacers++;
                    }
                }
                else
                {
                    if (c == 'K')
                    {
                        if (Squares[60].Piece != null)
                        {
                            if (Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                Squares[60].Piece.Moved = false;
                            }
                        }

                        if (Squares[63].Piece != null)
                        {
                            if (Squares[63].Piece.PieceType == ChessPieceType.Rook)
                            {
                                Squares[63].Piece.Moved = false;
                            }
                        }

                        WhiteCastled = false;
                        WhiteCanCastle = true;
                    }
                    else if (c == 'Q')
                    {
                        if (Squares[60].Piece != null)
                        {
                            if (Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                Squares[60].Piece.Moved = false;
                            }
                        }

                        if (Squares[56].Piece != null)
                        {
                            if (Squares[56].Piece.PieceType == ChessPieceType.Rook)
                            {
                                Squares[56].Piece.Moved = false;
                            }
                        }
                        WhiteCanCastle = true;
                        WhiteCastled = false;
                    }
                    else if (c == 'k')
                    {
                        if (Squares[4].Piece != null)
                        {
                            if (Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                Squares[4].Piece.Moved = false;
                            }
                        }

                        if (Squares[7].Piece != null)
                        {
                            if (Squares[7].Piece.PieceType == ChessPieceType.Rook)
                            {
                                Squares[7].Piece.Moved = false;
                            }
                        }

                        BlackCastled = false;
                        BlackCanCastle = true;
                    }
                    else if (c == 'q')
                    {
                        if (Squares[4].Piece != null)
                        {
                            if (Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                Squares[4].Piece.Moved = false;
                            }
                        }

                        if (Squares[0].Piece != null)
                        {
                            if (Squares[0].Piece.PieceType == ChessPieceType.Rook)
                            {
                                Squares[0].Piece.Moved = false;
                            }
                        }

                        BlackCastled = false;
                        BlackCanCastle = true;
                    }
                    else if (c == ' ')
                    {
                        spacers++;
                    }
                    else if (c == '1' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 1);
                    }
                    else if (c == '2' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 2);
                    }
                    else if (c == '3' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 3);
                    }
                    else if (c == '4' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 4);
                    }
                    else if (c == '5' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 5);
                    }
                    else if (c == '6' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 6);
                    }
                    else if (c == '7' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 7);
                    }
                    else if (c == '8' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 8);
                    }
                    else if (c == '9' && spacers == 4)
                    {
                        FiftyMove = (byte)((FiftyMove * 10) + 9);
                    }
                    else if (c == '0' && spacers == 4)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 0);
                    }
                    else if (c == '1' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 1);
                    }
                    else if (c == '2' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 2);
                    }
                    else if (c == '3' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 3);
                    }
                    else if (c == '4' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 4);
                    }
                    else if (c == '5' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 5);
                    }
                    else if (c == '6' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 6);
                    }
                    else if (c == '7' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 7);
                    }
                    else if (c == '8' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 8);
                    }
                    else if (c == '9' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 9);
                    }
                    else if (c == '0' && spacers == 5)
                    {
                        MoveCount = (byte)((MoveCount * 10) + 0);
                        MoveCount = (byte)((MoveCount * 10) + 0);
                    }
                }
            }
            return this;
        }

        public Board CreateNewGameBoard()
        {
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

            return this;
        }

        public Board CreateEmptyBoard()
        {
            for (byte i = 0; i < 64; i++)
            {
                Squares[i] = new Square();
            }

            LastMove = new MoveContent();

            BlackCanCastle = true;
            WhiteCanCastle = true;

            return this;
        }

    }
}
