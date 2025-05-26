using Chess.Classes;
using Chess.Classes.Pieces;
using ChessGame;
using System;
using System.Windows.Forms;

namespace Chess
{
    public class ChessGame
    {
        private static ChessGame _instance;
        public Piece[,] Board;
        public bool IsWhiteTurn { get; set; }
        public (Position from, Position to)? LastMove;
        public Position WhiteKingPos;
        public Position BlackKingPos;
        public bool GameEnded;
        private const int Size = 8;
        private readonly PieceFactory _pieceFactory;
        private readonly GameLogger? _logger;


        // Подія для сповіщення про зміну стану гри
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;


        // Клас аргументів події зміни стану гри
        public class GameStateChangedEventArgs : EventArgs
        {
            public bool IsWhiteTurn { get; set; }
            public bool IsCheck { get; set; }
            public bool IsGameEnded { get; set; }
            public string LastMove { get; set; }
        }
       
        private ChessGame(GameLogger logger = null)
        {
            Board = new Piece[Size, Size];
            IsWhiteTurn = true;
            LastMove = null;
            GameEnded = false;
            _pieceFactory = new PieceFactory();
            _logger = logger;
            InitializeBoard();
            NotifyGameStateChanged();
        }

        // Публічне властивість для доступу до єдиного екземпляра гри
        public static ChessGame Instance
        {
            get
            {
                var logger = new GameLogger();
                if (_instance == null)
                    _instance = new ChessGame(logger);
                return _instance;
            }
        }


        // Метод ініціалізації ігрової дошки зі стандартним розміщенням фігур
        private void InitializeBoard()
        {
            for (int col = 0; col < Size; col++)
            {
                Board[1, col] = _pieceFactory.CreatePiece(PieceType.Pawn, new Position(col, 1), false);
                Board[6, col] = _pieceFactory.CreatePiece(PieceType.Pawn, new Position(col, 6), true);
            }

            // Чорні фігури
            Board[0, 0] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(0, 0), false);
            Board[0, 1] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(1, 0), false);
            Board[0, 2] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(2, 0), false);
            Board[0, 3] = _pieceFactory.CreatePiece(PieceType.Queen, new Position(3, 0), false);
            Board[0, 4] = _pieceFactory.CreatePiece(PieceType.King, new Position(4, 0), false);
            Board[0, 5] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(5, 0), false);
            Board[0, 6] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(6, 0), false);
            Board[0, 7] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(7, 0), false);


            // Білі фігури
            Board[7, 0] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(0, 7), true);
            Board[7, 1] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(1, 7), true);
            Board[7, 2] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(2, 7), true);
            Board[7, 3] = _pieceFactory.CreatePiece(PieceType.Queen, new Position(3, 7), true);
            Board[7, 4] = _pieceFactory.CreatePiece(PieceType.King, new Position(4, 7), true);
            Board[7, 5] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(5, 7), true);
            Board[7, 6] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(6, 7), true);
            Board[7, 7] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(7, 7), true);

            WhiteKingPos = new Position(4, 7);
            BlackKingPos = new Position(4, 0);
        }


        // Перезапускає гру
        public void RestartGame()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Board[row, col] = null;
                }
            }

            Board.Initialize();
            InitializeBoard();
            IsWhiteTurn = true;
            LastMove = null;
            GameEnded = false;
            NotifyGameStateChanged();
        }


        // Оновлює позицію короля після ходу
        public void UpdateKingPosition(Position pos, bool isWhite)
        {
            if (isWhite)
                WhiteKingPos.UpdatePosition(pos.X, pos.Y);
            else
                BlackKingPos.UpdatePosition(pos.X, pos.Y);
            NotifyGameStateChanged();
        }


        // Повертає позицію короля певного кольору
        public Position GetKingPosition(bool isWhite)
        {
            return isWhite ? WhiteKingPos : BlackKingPos;
        }


        // Перевіряє, чи знаходиться клітинка під атакою суперника
        public bool IsSquareUnderAttack(Position targetPos, bool attackerIsWhite)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Piece piece = Board[row, col];
                    if (piece != null && piece.IsWhite == attackerIsWhite)
                    {
                        if (piece.IsValidMove(targetPos, Board, isCheckEvaluation: true))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        // Перевіряє, чи знаходиться король під шахом
        public bool IsKingInCheck(bool forWhite)
        {
            Position kingPos = GetKingPosition(forWhite);
            return IsSquareUnderAttack(kingPos, !forWhite);
        }



        public bool IsCastlingMove(Position start, Position end, Piece[,] board)
        {
            Piece piece = board[start.Y, start.X];
            if (piece is not King king || king.HasMoved || Math.Abs(end.X - start.X) != 2)
                return false;

            int direction = end.X > start.X ? 1 : -1;
            int rookX = direction == 1 ? 7 : 0;
            Piece rook = board[start.Y, rookX];

            if (rook is not Rook r || r.HasMoved || r.IsWhite != king.IsWhite)
                return false;

            // Перевірка, що клітини між королем і турою порожні
            int min = Math.Min(start.X, rookX) + 1;
            int max = Math.Max(start.X, rookX) - 1;
            for (int x = min; x <= max; x++)
                if (board[start.Y, x] != null)
                    return false;

            // Перевірка, що жодна клітина не під шахом (включаючи проміжну)
            Position[] positionsToCheck = new Position[]
            {
        new Position(start.X, start.Y),                         // поточна позиція короля
        new Position(start.X + direction, start.Y),            // проміжна клітина
        new Position(start.X + 2 * direction, start.Y)         // фінальна позиція
            };

            foreach (var pos in positionsToCheck)
            {
                if (IsSquareUnderAttack(pos, !king.IsWhite))
                    return false;
            }

            return true;
        }

        public void PerformCastling(Position kingStart, Position kingEnd, Piece[,] board)
        {
            int y = kingStart.Y;
            int direction = kingEnd.X > kingStart.X ? 1 : -1;
            int rookStartX = direction == 1 ? 7 : 0;
            int rookEndX = direction == 1 ? 5 : 3;

            Piece king = board[y, kingStart.X];
            Piece rook = board[y, rookStartX];

            board[y, kingStart.X] = null;
            board[y, rookStartX] = null;

            board[y, kingEnd.X] = king;
            board[y, rookEndX] = rook;

            king.UpdatePosition(new Position(kingEnd.X, y));
            rook.UpdatePosition(new Position(rookEndX, y));

            if (king is King k) k.HasMoved = true;
            if (rook is Rook r) r.HasMoved = true;

            UpdateKingPosition(kingEnd, king.IsWhite);
            IsWhiteTurn = !IsWhiteTurn;
            LastMove = (kingStart, kingEnd);
        }





        // Симулює хід, щоб перевірити, чи він рятує від шаху
        public bool IsMoveResolvingCheck(Position startPos, Position endPos, bool forWhite)
        {
            Position kingPos = GetKingPosition(forWhite);
            Piece piece = Board[startPos.Y, startPos.X];
            Piece capturedPiece = Board[endPos.Y, endPos.X];
            Board[endPos.Y, endPos.X] = piece;
            Board[startPos.Y, startPos.X] = null;

            if (piece is King)
            {
                kingPos = endPos;
            }

            bool stillInCheck = IsSquareUnderAttack(kingPos, !forWhite);


            // Відновлюємо дошку
            Board[startPos.Y, startPos.X] = piece;
            Board[endPos.Y, endPos.X] = capturedPiece;

            return !stillInCheck;
        }


        // Перевіряє, чи є в гравця хоч один дійсний хід
        public bool HasLegalMoves(bool forWhite)
        {
            // Перебираємо всі клітинки дошки
            for (int startRow = 0; startRow < Size; startRow++)
            {
                for (int startCol = 0; startCol < Size; startCol++)
                {
                    Piece piece = Board[startRow, startCol];

                    if (piece == null || piece.IsWhite != forWhite)
                        continue;

                    // Перевіряємо всі можливі цільові клітинки для ходу
                    for (int targetRow = 0; targetRow < Size; targetRow++)
                    {
                        for (int targetCol = 0; targetCol < Size; targetCol++)
                        {
                            // Якщо фігура не може легально рухатись на цю клітинку — пропускаємо
                            if (!CanMove(piece, startRow, startCol, targetRow, targetCol))
                                continue;

                            // Симулюємо хід і перевіряємо, чи не призведе він до шаху
                            if (IsLegalMove(piece, startRow, startCol, targetRow, targetCol, forWhite))
                                return true; 
                        }
                    }
                }
            }
            // Якщо легальних ходів не знайдено — повертаємо false
            return false;
        }


        // Перевіряє, чи фігура може рухатись з початкової в цільову позицію за правилами руху та чи там немає своєї фігури
        private bool CanMove(Piece piece, int startRow, int startCol, int targetRow, int targetCol)
        {
            Piece targetPiece = Board[targetRow, targetCol];
            Position targetPos = new Position(targetCol, targetRow);

            // Перевірка валідності ходу для фігури та що цільова клітинка або порожня, або з фігурою противника
            return piece.IsValidMove(targetPos, Board) &&
                   (targetPiece == null || targetPiece.IsWhite != piece.IsWhite);
        }


        // Симулює хід, перевіряє, чи після нього король не потрапляє під шах, потім відкатує хід
        private bool IsLegalMove(Piece piece, int startRow, int startCol, int targetRow, int targetCol, bool forWhite)
        {
            Piece tempPiece = Board[targetRow, targetCol];

            Board[targetRow, targetCol] = piece;
            Board[startRow, startCol] = null;

            Position originalKingPos = GetKingPosition(forWhite);

            Position kingPosToCheck = piece is King ? new Position(targetCol, targetRow) : originalKingPos;

            bool wouldBeInCheck = IsSquareUnderAttack(kingPosToCheck, !forWhite);

            Board[startRow, startCol] = piece;
            Board[targetRow, targetCol] = tempPiece;

            return !wouldBeInCheck;
        }



        // Перевірка стану гри: шах, мат, пат
        public void CheckGameState(BoardPanel boardPanel)
        {
            Position kingPos = GetKingPosition(IsWhiteTurn);
            bool isInCheck = IsSquareUnderAttack(kingPos, !IsWhiteTurn);
            bool hasLegalMoves = HasLegalMoves(IsWhiteTurn);

            if (isInCheck)
            {
                if (!hasLegalMoves)
                {
                    EndGame(!IsWhiteTurn ? "White wins by checkmate!" : "Black wins by checkmate!", boardPanel);
                }
            }
            else if (!hasLegalMoves)
            {
                EndGame("Stalemate! The game is a draw.", boardPanel);
            }
            NotifyGameStateChanged();
        }


        // Завершення гри з повідомленням
        public void EndGame(string message, BoardPanel boardPanel)
        {
            GameEnded = true;
            _logger?.LogGameResult(message);
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (MessageBox.Show("Would you like to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RestartGame();

                // Перемалювати панель
                boardPanel.Invalidate();
                if (boardPanel.FindForm() is GameForm gameForm)
                {

                    // Скинути таймери
                    gameForm.ResetTimers();
                }
            }
            NotifyGameStateChanged();
        }


        // Метод для сповіщення про зміну стану гри
        private void NotifyGameStateChanged()
        {
            GameStateChanged?.Invoke(this, new GameStateChangedEventArgs
            {
                IsWhiteTurn = IsWhiteTurn,
                IsCheck = IsKingInCheck(IsWhiteTurn),
                IsGameEnded = GameEnded,
                LastMove = LastMove.HasValue ? $"{LastMove.Value.from} to {LastMove.Value.to}" : null,
            });
        }
    }
}