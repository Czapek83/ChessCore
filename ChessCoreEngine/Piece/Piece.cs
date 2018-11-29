using System;
using System.Collections.Generic;

namespace ChessEngine.Engine
{
    public static class ChessPieceColor
    {
        public static readonly ChessColor White = new ChessColor { Colour = true };
        public static readonly ChessColor Black = new ChessColor { Colour = false };
    }

    public struct ChessColor
    {
        public bool Colour;

        public override bool Equals(object obj)
        {
            if (obj is ChessColor)
                return (Colour.Equals(((ChessColor)obj).Colour));
            return Colour.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Colour.GetHashCode();
        }

        public override string ToString()
        {
            return Colour.ToString();
        }

        public static bool operator== (ChessColor a, ChessColor b)
        {
            return a.Colour == b.Colour;
        }

        public static bool operator !=(ChessColor a, ChessColor b)
        {
            return a.Colour != b.Colour;
        }

        public static ChessColor operator !(ChessColor a)
        {
            var result = a == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;
            return result;
        }
    }

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

    public enum GenerateValidMovesPriority
    {
        Lowest,
        Low,
        Medium,
        High,
        Highest
    }

    public abstract class Piece
    {
        #region InternalMembers

        public ChessColor PieceColor { get; protected set; }
        public ChessPieceType PieceType { get; protected set; }

        public virtual short PieceValue { get; }
        public virtual short PieceActionValue { get; }

        internal short AttackedValue;
        internal short DefendedValue;
        
        internal int LastValidMoveCount;
        internal bool Moved;

        internal bool Selected;

        internal Stack<byte> ValidMoves;

        virtual internal GenerateValidMovesPriority GetGenerateValidMovesPriority(ChessColor colorThatIsOnMove)
        {
            return GenerateValidMovesPriority.Medium;
        }


        #endregion

        protected ICoordinatesConverter _coordinatesConverter;

        #region Constructors

        protected Piece(ChessPieceType chessPieceType, ChessColor chessPieceColor, ICoordinatesConverter coordinatesConverter)
        {
            PieceType = chessPieceType;
            PieceColor = chessPieceColor;
            _coordinatesConverter = coordinatesConverter;
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

        protected bool AnalyzeMove(byte dstPos, Board board)
        {
            //If I am not a pawn everywhere I move I can attack
            board.AttackBoard[PieceColor][dstPos] = true;

            //If there no piece there I can potentialy kill just add the move and exit
            if (board.GetPiece(dstPos) == null)
            {
                ValidMoves.Push(dstPos);

                return true;
            }

            Piece pcAttacked = board.GetPiece(dstPos);

            //if that piece is a different color
            if (pcAttacked.PieceColor != PieceColor)
            {
                pcAttacked.AttackedValue += PieceActionValue;

                //If this is a king set it in check                   
                if (pcAttacked.PieceType == ChessPieceType.King)
                {
                    board.SetCheckedSide(pcAttacked.PieceColor);
                }
                else
                {
                    //Add this as a valid move
                    ValidMoves.Push(dstPos);
                }


                //We don't continue movement past this piece
                return false;
            }
            //Same Color I am defending
            pcAttacked.DefendedValue += PieceActionValue;

            //Since this piece is of my kind I can't move there
            return false;
        }

        public virtual void GenerateMoves(byte piecePosition, Board board)
        {

        }
    }
}