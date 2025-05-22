using ChessGame.Classes;
using ChessGame.Classes.Pieces;
using System.Xml.Linq;

namespace ChessGame.Classes.Pieces
{
	public class Bishop : Piece
	{
		public Bishop(Position position, bool isWhite, Image icon) : base(position, isWhite)
		{
			Name = "Bishop";
			Icon = icon;
		}

        // Метод перевірки допустимості ходу слона
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {

            // Слон може рухатись лише по діагоналі,
            // отже зміщення по X має дорівнювати зміщенню по Y
            if (Math.Abs(endPos.X - Position.X) != Math.Abs(endPos.Y - Position.Y))
                return false;

            // Визначаємо напрямок руху по колонках і рядках
            int stepCol = endPos.X > Position.X ? 1 : -1;
            int stepRow = endPos.Y > Position.Y ? 1 : -1;

            // Початкові координати для перевірки шляху
            int col = Position.X + stepCol;
            int row = Position.Y + stepRow;

            // Перевіряємо, чи немає фігур на шляху
            while (true)
            {
                if (col == endPos.X && row == endPos.Y)
                    break;

                // Перевірка виходу за межі дошки
                if (col < 0 || col >= 8 || row < 0 || row >= 8)
                    return false;

                // Якщо клітинка не порожня — хід недійсний
                if (board[row, col] != null)
                    return false;

                col += stepCol;
                row += stepRow;
            }

            return true; // Якщо шлях вільний — хід допустимий
        }
	}
}