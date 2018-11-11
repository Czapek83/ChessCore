using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class FenBoardFactory : IBoardFactory
    {
        private readonly string _fen;

        public FenBoardFactory(string fen)
        {
            _fen = fen;
        }

        public Board CreateBoard()
        {
            var result = new Board();

            byte index = 0;

            result.WhiteCastled = true;
            result.BlackCastled = true;
            result.WhiteCanCastle = false;
            result.BlackCanCastle = false;

            byte spacers = 0;

            result.WhoseMove = ChessPieceColor.White;

            if (_fen.Contains("a3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 40;
            }
            else if (_fen.Contains("b3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 41;
            }
            else if (_fen.Contains("c3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 42;
            }
            else if (_fen.Contains("d3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 43;
            }
            else if (_fen.Contains("e3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 44;
            }
            else if (_fen.Contains("f3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 45;
            }
            else if (_fen.Contains("g3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 46;
            }
            else if (_fen.Contains("h3"))
            {
                result.EnPassantColor = ChessPieceColor.White;
                result.EnPassantPosition = 47;
            }


            if (_fen.Contains("a6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 16;
            }
            else if (_fen.Contains("b6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 17;
            }
            else if (_fen.Contains("c6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 18;
            }
            else if (_fen.Contains("d6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 19;
            }
            else if (_fen.Contains("e6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 20;
            }
            else if (_fen.Contains("f6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 21;
            }
            else if (_fen.Contains("g6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 22;
            }
            else if (_fen.Contains("h6"))
            {
                result.EnPassantColor = ChessPieceColor.Black;
                result.EnPassantPosition = 23;
            }

            if (_fen.Contains(" w "))
            {
                result.WhoseMove = ChessPieceColor.White;
            }
            if (_fen.Contains(" b "))
            {
                result.WhoseMove = ChessPieceColor.Black;
            }

            foreach (char c in _fen)
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
                        result.Squares[index].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'N')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'B')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'R')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'Q')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'K')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.King, ChessPieceColor.White);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'p')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Pawn, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'n')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Knight, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'b')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Bishop, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'r')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Rook, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'q')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.Queen, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
                        index++;
                    }
                    else if (c == 'k')
                    {
                        result.Squares[index].Piece = new Piece(ChessPieceType.King, ChessPieceColor.Black);
                        result.Squares[index].Piece.Moved = true;
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
                        if (result.Squares[60].Piece != null)
                        {
                            if (result.Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[60].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[63].Piece != null)
                        {
                            if (result.Squares[63].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[63].Piece.Moved = false;
                            }
                        }

                        result.WhiteCastled = false;
                        result.WhiteCanCastle = true;
                    }
                    else if (c == 'Q')
                    {
                        if (result.Squares[60].Piece != null)
                        {
                            if (result.Squares[60].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[60].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[56].Piece != null)
                        {
                            if (result.Squares[56].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[56].Piece.Moved = false;
                            }
                        }
                        result.WhiteCanCastle = true;
                        result.WhiteCastled = false;
                    }
                    else if (c == 'k')
                    {
                        if (result.Squares[4].Piece != null)
                        {
                            if (result.Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[4].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[7].Piece != null)
                        {
                            if (result.Squares[7].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[7].Piece.Moved = false;
                            }
                        }

                        result.BlackCastled = false;
                        result.BlackCanCastle = true;
                    }
                    else if (c == 'q')
                    {
                        if (result.Squares[4].Piece != null)
                        {
                            if (result.Squares[4].Piece.PieceType == ChessPieceType.King)
                            {
                                result.Squares[4].Piece.Moved = false;
                            }
                        }

                        if (result.Squares[0].Piece != null)
                        {
                            if (result.Squares[0].Piece.PieceType == ChessPieceType.Rook)
                            {
                                result.Squares[0].Piece.Moved = false;
                            }
                        }

                        result.BlackCastled = false;
                        result.BlackCanCastle = true;
                    }
                    else if (c == ' ')
                    {
                        spacers++;
                    }
                    else if (c == '1' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 1);
                    }
                    else if (c == '2' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 2);
                    }
                    else if (c == '3' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 3);
                    }
                    else if (c == '4' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 4);
                    }
                    else if (c == '5' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 5);
                    }
                    else if (c == '6' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 6);
                    }
                    else if (c == '7' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 7);
                    }
                    else if (c == '8' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 8);
                    }
                    else if (c == '9' && spacers == 4)
                    {
                        result.FiftyMove = (byte)((result.FiftyMove * 10) + 9);
                    }
                    else if (c == '0' && spacers == 4)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                    }
                    else if (c == '1' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 1);
                    }
                    else if (c == '2' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 2);
                    }
                    else if (c == '3' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 3);
                    }
                    else if (c == '4' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 4);
                    }
                    else if (c == '5' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 5);
                    }
                    else if (c == '6' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 6);
                    }
                    else if (c == '7' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 7);
                    }
                    else if (c == '8' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 8);
                    }
                    else if (c == '9' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 9);
                    }
                    else if (c == '0' && spacers == 5)
                    {
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                        result.MoveCount = (byte)((result.MoveCount * 10) + 0);
                    }
                }
            }
            return result;
        }
    }
}
