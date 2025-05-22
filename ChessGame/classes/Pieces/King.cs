using ChessGame.Classes;
using ChessGame.Classes.Pieces;
using System.Xml.Linq;


namespace ChessGame.Classes.Pieces
{
	public class King : Piece
	{
		public King(Position position, bool isWhite, Image icon) : base(position, isWhite)
		{
			Name = "King";
			Icon = icon;
		}

        // Метод перевірки допустимості ходу короля
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {

            // Обчислюємо абсолютне зміщення по колонках і рядках
            int deltaCol = Math.Abs(endPos.X - Position.X);
            int deltaRow = Math.Abs(endPos.Y - Position.Y);

            // Король може ходити на 1 клітинку в будь-якому напрямку, окрім того, де не рухається
            return deltaCol <= 1 && deltaRow <= 1 && (deltaCol != 0 || deltaRow != 0);
        }
    }
}