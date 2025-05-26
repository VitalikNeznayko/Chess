using Chess.Classes.Pieces;
using Chess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class MoveValidator
    {
        public static bool IsMoveResolvingCheck(Piece[,] board, Position startPos, Position endPos, bool forWhite, Position kingPos)
        {
            Piece piece = board[startPos.Y, startPos.X];
            Piece capturedPiece = board[endPos.Y, endPos.X];
            board[endPos.Y, endPos.X] = piece;
            board[startPos.Y, startPos.X] = null;

            if (piece is King)
                kingPos = endPos;

            bool stillInCheck = BoardEvaluator.IsSquareUnderAttack(board, kingPos, !forWhite);

            board[startPos.Y, startPos.X] = piece;
            board[endPos.Y, endPos.X] = capturedPiece;

            return !stillInCheck;
        }

        public static bool HasLegalMoves(Piece[,] board, bool forWhite, Position kingPos)
        {
            for (int startRow = 0; startRow < 8; startRow++)
            {
                for (int startCol = 0; startCol < 8; startCol++)
                {
                    Piece piece = board[startRow, startCol];
                    if (piece == null || piece.IsWhite != forWhite) continue;

                    for (int targetRow = 0; targetRow < 8; targetRow++)
                    {
                        for (int targetCol = 0; targetCol < 8; targetCol++)
                        {
                            if (!CanMove(piece, board, startRow, startCol, targetRow, targetCol))
                                continue;
                            if (IsLegalMove(piece, board, startRow, startCol, targetRow, targetCol, forWhite, kingPos))
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool CanMove(Piece piece, Piece[,] board, int startRow, int startCol, int targetRow, int targetCol)
        {
            Piece targetPiece = board[targetRow, targetCol];
            Position targetPos = new Position(targetCol, targetRow);
            return piece.IsValidMove(targetPos, board) && (targetPiece == null || targetPiece.IsWhite != piece.IsWhite);
        }

        private static bool IsLegalMove(Piece piece, Piece[,] board, int startRow, int startCol, int targetRow, int targetCol, bool forWhite, Position kingPos)
        {
            Piece tempPiece = board[targetRow, targetCol];
            board[targetRow, targetCol] = piece;
            board[startRow, startCol] = null;

            Position kingPosToCheck = piece is King ? new Position(targetCol, targetRow) : kingPos;
            bool wouldBeInCheck = BoardEvaluator.IsSquareUnderAttack(board, kingPosToCheck, !forWhite);

            board[startRow, startCol] = piece;
            board[targetRow, targetCol] = tempPiece;

            return !wouldBeInCheck;
        }
    }
}
