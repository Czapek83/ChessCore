namespace ChessEngine.Engine
{
    
    internal struct Square
    {
        internal Piece Piece;

        #region Constructors

        internal Square(Piece piece)
        {
            Piece = PieceFactory.CreatePieceByTypeAndColor(piece.PieceType, piece.PieceColor);
        }

        #endregion
    }
}