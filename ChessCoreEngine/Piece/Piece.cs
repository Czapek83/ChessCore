using System;
using System.Collections.Generic;

namespace ChessEngine.Engine.Pieces
{
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
            bool endGamePhase, PawnCountDictionary pawnCount)
        {
            int score = 0;

            byte index = _coordinatesConverter.GetPositionByChessColor(position, PieceColor);

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
                pawnCount);
            
            return score;
        }

        public abstract int EvaluatePieceSpecificScore(byte position,
            bool endGamePhase, byte index, PawnCountDictionary pawnCount);

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
                if (pcAttacked.PieceType != ChessPieceType.King)
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