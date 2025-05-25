using ChessGame.Classes;
using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public class KingMoveStrategy : IMoveStrategy
    {
        public bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false)
        {
            int dx = Math.Abs(endPos.X - currentPos.X);
            int dy = Math.Abs(endPos.Y - currentPos.Y);
            return (dx <= 1 && dy <= 1) && (dx != 0 || dy != 0);
        }
    }
}
