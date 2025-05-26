using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess.Classes.Pieces
{
	public class Pawn : Piece
	{
		public Pawn(Position position, bool isWhite, Image icon) : base(position, isWhite)
		{
			Name = "Pawn";
            Icon = icon;
        }


        // Метод для перевірки допустимості ходу пішака
        public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
		{
            // Напрямок руху пішака: для білих - вгору (-1), для чорних - вниз (+1)
            int direction = IsWhite ? -1 : 1;

            // Зміщення по колонці та рядку від поточної позиції до кінцевої
            int deltaCol = endPos.X - Position.X;
			int deltaRow = endPos.Y - Position.Y;

            // Якщо не виконується перевірка шаха (зазвичай звичайний хід)
            if (!isCheckEvaluation)
            {
                // Рух вперед на 2 клітинки з початкової позиції, якщо шлях чистий
                if (deltaCol == 0 && deltaRow == 2 * direction && board[endPos.Y, endPos.X] == null)
                {
                    // Пішак може зробити такий хід лише з початкової лінії (6 рядок для білих, 1 для чорних)
                    if ((IsWhite && Position.Y == 6) || (!IsWhite && Position.Y == 1))
                    {
                        int middleRow = Position.Y + direction;

                        // Переконуємося, що клітинка між початковою і кінцевою порожня
                        if (board[middleRow, Position.X] == null)
                        {
                            return true;
                        }
                    }
                }

                // Рух вперед на 1 клітинку, якщо шлях вільний
                if (deltaCol == 0 && deltaRow == direction && board[endPos.Y, endPos.X] == null)
                {
                    return true;
                }
            }

            // Хід по діагоналі на 1 клітинку (захоплення фігури супротивника)
            if (Math.Abs(deltaCol) == 1 && deltaRow == direction)
            {
                if (isCheckEvaluation)
                {
                    // Під час перевірки шаха дозволяємо хід по діагоналі без перевірки присутності фігури
                    return true;
                }
                else
                {
                    // Перевіряємо, чи на цій діагоналі є фігура суперника
                    if (board[endPos.Y, endPos.X] != null && board[endPos.Y, endPos.X].IsWhite != IsWhite)
                    {
                        return true;
                    }
                }
            }

            // Якщо жодна з умов не виконана — хід недопустимий
            return false;
        }
	}
}
