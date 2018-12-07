using ChessCoreEngine.Board;
using ChessEngine.Engine.Enums;
using ChessEngine.Engine.Loggers;
using ChessEngine.Engine.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class FenBoardFactory : BoardFactory
    {
        private readonly FenHelper _fenHelper;
        private readonly string _fen;

        public FenBoardFactory(FenHelper fenHelper, string fen, LoggerBase baseLogger): base(baseLogger)
        {
            _fenHelper = fenHelper ?? throw new ArgumentException(nameof(fenHelper));
            _fen = fen ?? throw new ArgumentException(nameof(fen));
        }

        public override Board CreateBoard()
        {
            _logger.LogInfo($"BoardFactory.CreateBoardFromFen({_fen}) start");

            byte index = 0;

            WhiteCastled = true;
            BlackCastled = true;
            WhiteCanCastle = false;
            BlackCanCastle = false;

            byte spacers = 0;

            WhoseMove = ChessPieceColor.White;

            var enPassantData = _fenHelper.GetEnPassantDataFromFen(_fen);
            if (enPassantData.HasValue)
            {
                EnPassantColor = enPassantData.Value.enPassantColor;
                EnPassantPosition = enPassantData.Value.enPassantPosition;
            }

            if (_fen.Contains(" w "))
            {
                WhoseMove = ChessPieceColor.White;
            }
            if (_fen.Contains(" b "))
            {
                WhoseMove = ChessPieceColor.Black;
            }

            foreach (char c in _fen)
            {

                if (index < 64 && spacers == 0)
                {
                    var code = (byte)c;
                    var isNumber = char.IsNumber(c);
                    var charToUpper = char.ToUpper(c);

                    if (isNumber)
                    {
                        byte charToByte = (byte)(code - 48);

                        if (index < 64 - charToByte)
                        {
                            index += charToByte;
                        }
                    }
                    else if (charToUpper == 'P' || charToUpper == 'N' || charToUpper == 'B' ||
                        charToUpper == 'R' || charToUpper == 'Q' || charToUpper == 'K')
                    {

                        Squares[index].Piece = PieceFactory.CreatePieceByFenCode(c);
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

            _logger.LogDebug($"BoardFactory.CreateBoardFromFen() end {this}");

            return this;
        }

    }
}
