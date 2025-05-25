using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public interface IMoveStrategy
    {
        bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false);
    }
}
