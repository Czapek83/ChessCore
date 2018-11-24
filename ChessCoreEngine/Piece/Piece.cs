using System;
using System.Collections.Generic;

namespace ChessEngine.Engine
{

    #region Enums

    #region ChessPieceColor enum

    public enum ChessPieceColor
    {
        White,
        Black
    }

    #endregion

    #region ChessPieceType enum

    public enum ChessPieceType
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn,
        None
    }

    #endregion

    #endregion
    
    public class Piece
    {
        #region InternalMembers

        internal ChessPieceColor PieceColor;
        internal ChessPieceType PieceType;

        public virtual short PieceValue { get; }
        public virtual short PieceActionValue { get; }

        internal short AttackedValue;
        internal short DefendedValue;
        
        internal int LastValidMoveCount;
        internal bool Moved;

        internal bool Selected;

        internal Stack<byte> ValidMoves;

        #endregion

        #region Constructors

        protected Piece(Piece piece)
        {
            PieceColor = piece.PieceColor;
            PieceType = piece.PieceType;
            Moved = piece.Moved;
            PieceValue = piece.PieceValue;
            PieceActionValue = piece.PieceActionValue;
            
            if (piece.ValidMoves != null)
                LastValidMoveCount = piece.ValidMoves.Count;                      
        }

        protected Piece(ChessPieceType chessPiece, ChessPieceColor chessPieceColor)
        {
            PieceType = chessPiece;
            PieceColor = chessPieceColor;

            if (PieceType == ChessPieceType.Pawn || PieceType == ChessPieceType.Knight)
            {
                LastValidMoveCount = 2;
            }
            else
            {
                LastValidMoveCount = 0;
            }

            ValidMoves = new Stack<byte>(LastValidMoveCount);

            PieceValue = CalculatePieceValue(PieceType);
            PieceActionValue = CalculatePieceActionValue(PieceType);
        }

        #endregion

        #region InternalMethods

        internal static short CalculatePieceValue(ChessPieceType pieceType)
        {
            switch (pieceType)
            {
                case ChessPieceType.Pawn:
                    {
                        return 100;
                        
                    }
                case ChessPieceType.Knight:
                    {
                        return 320;
                    }
                case ChessPieceType.Bishop:
                    {
                        return 325;
                    }
                case ChessPieceType.Rook:
                    {
                        return 500;
                    }

                case ChessPieceType.Queen:
                    {
                        return 975;
                    }

                case ChessPieceType.King:
                    {
                        return 32767;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        public static Piece CreatePieceByTypeAndColor(ChessPieceType chesspieceType,
            ChessPieceColor chessPieceColor)
        {
            switch (chesspieceType)
            {
                case ChessPieceType.Bishop:
                    return new Bishop(chessPieceColor);
                case ChessPieceType.Knight:
                    return new Knight(chessPieceColor);
                case ChessPieceType.Rook:
                    return new Rook(chessPieceColor);
                case ChessPieceType.Queen:
                    return new Queen(chessPieceColor);
                case ChessPieceType.King:
                    return new King(chessPieceColor);
                case ChessPieceType.Pawn:
                    return new Pawn(chessPieceColor);
                default: throw new ArgumentException($"Invalid chessPieceType {chesspieceType}");
            }
        }

        internal static short CalculatePieceActionValue(ChessPieceType pieceType)
        {
            switch (pieceType)
            {
                case ChessPieceType.Pawn:
                    {
                        return 6;

                    }
                case ChessPieceType.Knight:
                    {
                        return 3;
                    }
                case ChessPieceType.Bishop:
                    {
                        return 3;
                    }
                case ChessPieceType.Rook:
                    {
                        return 2;
                    }

                case ChessPieceType.Queen:
                    {
                        return 1;
                    }

                case ChessPieceType.King:
                    {
                        return 1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        #endregion

        public virtual string GetPieceTypeShort()
        {
            return String.Empty;
        }

        public virtual bool IsKing()
        {
            return false;
        }

        public new string ToString()
        {
            return GetPieceTypeShort() + " " + PieceColor + " " + PieceValue + " " + PieceActionValue + " " + ValidMoves.Count + " " + AttackedValue + " " + DefendedValue;

        }

    }
}