using ChessGame.Classes;
using ChessGame.Classes.Pieces;
using System.Xml.Linq;


namespace ChessGame.Classes.Pieces
{
	public class Queen : Piece
	{
		public Queen(Position position, bool isWhite, Image icon) : base(position, isWhite)
		{
			Name = "Queen";
			Icon = icon;
		}


        // Метод перевірки, чи є хід допустимим
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
		{
            // Ферзь рухається, як тура (по вертикалі чи горизонталі)
            bool movesLikeRook = Position.X == endPos.X || Position.Y == endPos.Y;

            // або як слон (по діагоналі — рівна кількість клітин по X і Y)
            bool movesLikeBishop = Math.Abs(endPos.X - Position.X) == Math.Abs(endPos.Y - Position.Y);

            // Якщо хід не відповідає жодному з варіантів — недійсний
            if (!movesLikeRook && !movesLikeBishop)
				return false;

			int stepCol = 0, stepRow = 0;

            // Обчислення напрямку руху по колонці та рядку
            if (movesLikeRook)
			{
				stepCol = Position.X == endPos.X ? 0 : (endPos.X > Position.X ? 1 : -1);
				stepRow = Position.Y == endPos.Y ? 0 : (endPos.Y > Position.Y ? 1 : -1);
			}
			else if (movesLikeBishop)
			{
				stepCol = endPos.X > Position.X ? 1 : -1;
				stepRow = endPos.Y > Position.Y ? 1 : -1;
			}

            // Починаємо перевірку клітинок між початковою та кінцевою позиціями
            int col = Position.X + stepCol;
			int row = Position.Y + stepRow;

			while (true)
			{
                // Якщо дійшли до кінцевої позиції — вихід з циклу
                if (col == endPos.X && row == endPos.Y)
					break;

                // Перевірка на вихід за межі дошки
                if (col < 0 || col >= 8 || row < 0 || row >= 8)
					return false;

                // Якщо на шляху є фігура — хід недійсний
                if (board[row, col] != null)
					return false;

                // Рухаємося далі в напрямку ходу
                col += stepCol;
				row += stepRow;
			}

            // Якщо шлях чистий — хід допустимий
            return true;
		}
	}
}