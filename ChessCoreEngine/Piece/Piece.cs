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
    
    public abstract class Piece
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

        public int EvaluatePieceScore(byte position, 
            bool endGamePhase, short[] whitePawnTable, short[] blackPawnTable)
        {
            int score = 0;

            byte index = position;

            if (PieceColor == ChessPieceColor.Black)
            {
                index = (byte)(63 - position);
            }

            //Calculate Piece Values
            score += PieceValue;
            score += DefendedValue;
            score -= AttackedValue;

            //Double Penalty for Hanging Pieces
            if (DefendedValue < AttackedValue)
            {
                score -= ((AttackedValue - DefendedValue) * 10);
            }

            //Add Points for Mobility
            if (ValidMoves != null)
            {
                score += ValidMoves.Count;
            }

            score += EvaluatePieceSpecificScore(position, endGamePhase, index, 
                whitePawnTable, blackPawnTable);
            
            return score;
        }

        public abstract int EvaluatePieceSpecificScore(byte position,
            bool endGamePhase, byte index, short[] whitePawnTable, short[] blackPawnTable);
    }
}