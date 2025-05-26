using Chess.Classes;
using Chess.Classes.Pieces;
using System.Xml.Linq;

namespace Chess.Classes.Pieces
{
    public class Knight : Piece
    {
        public Knight(Position position, bool isWhite, Image icon) : base(position, isWhite)
        {
            Name = "Knight";
            Icon = icon;
        }

        // Метод перевірки допустимості ходу коня
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {
            // Обчислюємо абсолютне зміщення по колонках і рядках
            int deltaCol = Math.Abs(endPos.X - Position.X);
            int deltaRow = Math.Abs(endPos.Y - Position.Y);

            // Конь ходить літерою "Г": 2 клітинки в одному напрямку і 1 в іншому
            return (deltaCol == 2 && deltaRow == 1) || (deltaCol == 1 && deltaRow == 2);
        }
    }
}
