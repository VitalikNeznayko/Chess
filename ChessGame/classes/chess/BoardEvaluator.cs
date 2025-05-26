using Chess.Classes.Pieces;
using Chess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class BoardEvaluator
    {
        public static bool IsSquareUnderAttack(Piece[,] board, Position targetPos, bool attackerIsWhite)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    if (piece != null && piece.IsWhite == attackerIsWhite)
                    {
                        if (piece.IsValidMove(targetPos, board, isCheckEvaluation: true))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
