using ChessGame.Classes;
using ChessGame.Classes.Pieces;

namespace ChessGame.Classes.Strategies
{
    public class PawnMoveStrategy : IMoveStrategy
    {
        public bool IsValidMove(Position currentPos, Position endPos, Piece[,] board, bool isWhite, bool isCheckEvaluation = false)
        {
            int direction = isWhite ? -1 : 1;
            int deltaX = endPos.X - currentPos.X;
            int deltaY = endPos.Y - currentPos.Y;

            if (!isCheckEvaluation)
            {
                if (deltaX == 0 && deltaY == 2 * direction && board[endPos.Y, endPos.X] == null)
                {
                    if ((isWhite && currentPos.Y == 6) || (!isWhite && currentPos.Y == 1))
                    {
                        int midRow = currentPos.Y + direction;
                        if (board[midRow, currentPos.X] == null)
                            return true;
                    }
                }

                if (deltaX == 0 && deltaY == direction && board[endPos.Y, endPos.X] == null)
                    return true;
            }

            if (Math.Abs(deltaX) == 1 && deltaY == direction)
            {
                if (isCheckEvaluation) return true;

                Piece target = board[endPos.Y, endPos.X];
                if (target != null && target.IsWhite != isWhite)
                    return true;
            }

            return false;
        }
    }
}
