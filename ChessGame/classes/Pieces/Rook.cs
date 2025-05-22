using System;
using System.Drawing;

namespace ChessGame.Classes.Pieces
{
    public class Rook : Piece
    {
        public bool HasMoved { get; set; }

        public Rook(Position position, bool isWhite, Image icon) : base(position, isWhite)
        {
            Name = "Rook";
            Icon = icon;
            HasMoved = false;
        }

        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {
            if (Position.X != endPos.X && Position.Y != endPos.Y)
                return false;

            int stepCol = Position.X == endPos.X ? 0 : (endPos.X > Position.X ? 1 : -1);
            int stepRow = Position.Y == endPos.Y ? 0 : (endPos.Y > Position.Y ? 1 : -1);
            int col = Position.X + stepCol;
            int row = Position.Y + stepRow;

            while (true)
            {
                if (col == endPos.X && row == endPos.Y)
                    break;

                if (col < 0 || col >= 8 || row < 0 || row >= 8)
                    return false;

                if (board[row, col] != null)
                    return false;

                col += stepCol;
                row += stepRow;
            }

            return true;
        }
    }
}