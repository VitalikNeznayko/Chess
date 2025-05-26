using Chess.Classes.Pieces;
using Chess.Classes;
using System.Xml.Linq;

namespace Chess.Classes.Pieces
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

            int dx = Math.Abs(endPos.X - Position.X);
            int dy = Math.Abs(endPos.Y - Position.Y);

            // Звичайний хід короля
            if ((dx <= 1 && dy <= 1) && (dx + dy > 0))
                return true;

            // Рокіровка
            if (!HasMoved && dy == 0 && dx == 2)
            {
                int direction = endPos.X > Position.X ? 1 : -1;
                int rookX = direction == 1 ? 7 : 0;
                Piece rook = board[Position.Y, rookX];

                if (rook is Rook r && !r.HasMoved && r.IsWhite == this.IsWhite)
                {
                    // Перевірити, що всі клітини між королем і турою порожні
                    int min = Math.Min(Position.X, rookX) + 1;
                    int max = Math.Max(Position.X, rookX) - 1;
                    for (int x = min; x <= max; x++)
                        if (board[Position.Y, x] != null)
                            return false;

                    return true; // Рокіровка можлива
                }
            }

            return false;
        }
    }
}