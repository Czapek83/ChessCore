
using ChessEngine.Engine.Enums;

namespace ChessEngine.Engine
{
    public class EvaluationParameters
    {
        public bool IsDraw { get; internal set; }
        public int FiftyMove { get; internal set; }
        public int RepeatedMove { get; internal set; }
        public bool BlackMate { get; internal set; }
        public bool WhiteMate { get; internal set; }
        public bool BlackCheck { get; internal set; }
        public bool EndGamePhase { get; internal set; }
        public bool WhiteCheck { get; internal set; }
        public bool BlackCastled { get; internal set; }
        public bool WhiteCastled { get; internal set; }
        public ChessColor WhoseMove { get; internal set; }
        public bool InsufficientMaterial { get; internal set; }
        public bool WhiteCanCastle { get; internal set; }
        public bool BlackCanCastle { get; internal set; }
    }
}
