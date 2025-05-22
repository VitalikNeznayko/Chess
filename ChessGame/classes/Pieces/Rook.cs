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

        // Метод перевірки правильності ходу
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {
            // Тура може рухатися лише по горизонталі або вертикалі
            if (Position.X != endPos.X && Position.Y != endPos.Y)
                return false;

            // Визначення напрямку руху по X та Y
            int stepCol = Position.X == endPos.X ? 0 : (endPos.X > Position.X ? 1 : -1);
            int stepRow = Position.Y == endPos.Y ? 0 : (endPos.Y > Position.Y ? 1 : -1);
            int col = Position.X + stepCol;
            int row = Position.Y + stepRow;


            // Перевірка, чи шлях до кінцевої позиції не заблокований іншими фігурами
            while (true)
            {
                // Досягнуто кінцевої позиції — вихід з циклу
                if (col == endPos.X && row == endPos.Y)
                    break;

                // Перевірка на вихід за межі дошки
                if (col < 0 || col >= 8 || row < 0 || row >= 8)
                    return false;

                // Якщо на шляху є інша фігура — хід недійсний
                if (board[row, col] != null)
                    return false;

                col += stepCol;
                row += stepRow;
            }

            // Хід допустимий, якщо шлях вільний
            return true;
        }
    }
}