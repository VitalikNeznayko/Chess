using ChessGame.Classes.Strategies;
using System.Drawing;

namespace ChessGame.Classes.Pieces
{
	public abstract class Piece
	{
        // Визначає, чи фігура біла. Якщо false – фігура чорна.
        public bool IsWhite { get; set; }
		public string Name { get; set; }
		public Image Icon { get; set; }
		public Position Position { get; private set; }

        public IMoveStrategy MoveStrategy { get; set; }


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
        public virtual bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
        {
            return MoveStrategy?.IsValidMove(Position, endPos, board, IsWhite, isCheckEvaluation) ?? false;
        }
    }
}