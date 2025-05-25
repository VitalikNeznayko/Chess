using ChessGame.Classes;
using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public class BishopMoveStrategy : IMoveStrategy
    {
        public bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false)
        {
            if (Math.Abs(endPos.X - currentPos.X) != Math.Abs(endPos.Y - currentPos.Y))
                return false;

            int stepCol = endPos.X > currentPos.X ? 1 : -1;
            int stepRow = endPos.Y > currentPos.Y ? 1 : -1;
            int col = currentPos.X + stepCol;
            int row = currentPos.Y + stepRow;

            while (col != endPos.X && row != endPos.Y)
            {
                if (board[row, col] != null)
                    return false;
                col += stepCol;
                row += stepRow;
            }

            return true;
        }
    }
}
