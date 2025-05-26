using ChessGame.Classes;
using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public class RookMoveStrategy : IMoveStrategy
    {
        public bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false)
        {
            if (currentPos.X != endPos.X && currentPos.Y != endPos.Y)
                return false;

            int stepX = currentPos.X == endPos.X ? 0 : (endPos.X > currentPos.X ? 1 : -1);
            int stepY = currentPos.Y == endPos.Y ? 0 : (endPos.Y > currentPos.Y ? 1 : -1);

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
