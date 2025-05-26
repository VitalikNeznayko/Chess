using System.Drawing;

namespace Chess.Classes.Pieces
{
	public abstract class Piece
	{
        // Визначає, чи фігура біла. Якщо false – фігура чорна.
        public bool IsWhite { get; set; }
		public string Name { get; set; }
		public Image Icon { get; set; }
        public bool HasMoved { get; set; } = false;
        public Position Position { get; private set; }

		protected Piece(Position position, bool isWhite)
		{
			Position = position;
			IsWhite = isWhite;
		}

        // Оновлює позицію фігури на нову.
        public void UpdatePosition(Position newPosition)
		{
			Position = newPosition;
		}

        // Абстрактний метод для перевірки, чи є хід на вказану позицію коректним.
        public abstract bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false);
	}
}