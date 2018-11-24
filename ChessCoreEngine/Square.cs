namespace ChessEngine.Engine
{
    
    internal struct Square
    {
        internal Piece Piece;

        #region Constructors

        internal Square(Piece piece)
        {
            Piece = Piece.CreatePieceByTypeAndColor(piece.PieceType, piece.PieceColor);
        }

        #endregion
    }
}