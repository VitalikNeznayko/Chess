using ChessGame.Classes;
using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public class QueenMoveStrategy : IMoveStrategy
    {
        public bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false)
        {
            int dx = endPos.X - currentPos.X;
            int dy = endPos.Y - currentPos.Y;

            bool isStraight = dx == 0 || dy == 0;
            bool isDiagonal = Math.Abs(dx) == Math.Abs(dy);

            if (!isStraight && !isDiagonal) return false;

            int stepX = dx == 0 ? 0 : (dx > 0 ? 1 : -1);
            int stepY = dy == 0 ? 0 : (dy > 0 ? 1 : -1);

            int col = currentPos.X + stepX;
            int row = currentPos.Y + stepY;

            while (col != endPos.X || row != endPos.Y)
            {
                if (board[row, col] != null)
                    return false;

                col += stepX;
                row += stepY;
            }

            return true;
        }
    }
}
