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

        public ChessPieceColor PieceColor { get; protected set; }
        public ChessPieceType PieceType { get; protected set; }

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

        protected Piece(ChessPieceType chessPieceType, ChessPieceColor chessPieceColor)
        {
            PieceType = chessPieceType;
            PieceColor = chessPieceColor;
            ValidMoves = new Stack<byte>(LastValidMoveCount);
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