using ChessGame.Classes.Pieces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGame.Classes
{
    public class BoardPanel : Panel
    {
        private const int Size = 8;
        private int cellSize = 60;
        private Position selectedPiece = null;
        private const int CoordinateOffset = 35;
        private readonly Font coordinateFont = new Font("Arial", 12, FontStyle.Bold);


        // Кисті для заливки клітинок
        private Brush lightBrush;
        private Brush darkBrush;
        private Brush backgroundBrush;


        // Кольори підсвічування
        private readonly struct HighlightStyles
        {
            public static readonly Brush Check = new SolidBrush(Color.FromArgb(255, 200, 0, 0));
            public static readonly Brush LastMove = new SolidBrush(Color.FromArgb(255, 240, 240, 10));
            public static readonly Brush ValidMove = new SolidBrush(Color.FromArgb(255, 185, 155, 255));
            public static readonly Brush CaptureMove = new SolidBrush(Color.FromArgb(255, 50, 180, 50));
        }


        public BoardPanel(int cellSize = 60, string background = "default")
        {
            this.cellSize = cellSize;
            this.Width = cellSize * Size + CoordinateOffset * 2;
            this.Height = cellSize * Size + CoordinateOffset * 2;
            SetTheme(background);
            this.DoubleBuffered = true;
            this.Paint += Board_Paint;
            this.MouseClick += BoardPanel_MouseClick;

            ChessGame.Instance.GameStateChanged += OnGameStateChanged;
        }


        // Встановлення колірної теми дошки
        private void SetTheme(string background)
        {
            switch (background.ToLower())
            {
                case "default":
                    lightBrush = new SolidBrush(Color.FromArgb(0xE8, 0xED, 0xF9));
                    darkBrush = new SolidBrush(Color.FromArgb(0xB7, 0xC0, 0xD8));
                    backgroundBrush = new SolidBrush(Color.FromArgb(230, 240, 255));
                    break;
                case "classic":
                    lightBrush = new SolidBrush(Color.FromArgb(240, 217, 181));
                    darkBrush = new SolidBrush(Color.FromArgb(181, 136, 99));
                    backgroundBrush = new SolidBrush(Color.Beige);
                    break;
                case "green":
                    lightBrush = new SolidBrush(Color.FromArgb(234, 235, 200));
                    darkBrush = new SolidBrush(Color.FromArgb(119, 149, 86));
                    backgroundBrush = new SolidBrush(Color.FromArgb(200, 255, 200));
                    break;
                default:
                    lightBrush = new SolidBrush(Color.FromArgb(0xE8, 0xED, 0xF9));
                    darkBrush = new SolidBrush(Color.FromArgb(0xB7, 0xC0, 0xD8));
                    backgroundBrush = new SolidBrush(Color.FromArgb(230, 240, 255));
                    break;
            }
        }


        // Подія зміни стану гри
        private void OnGameStateChanged(object sender, ChessGame.GameStateChangedEventArgs e)
        {
            // Перемальовуємо дошку
            Invalidate(); 
        }


        // Метод малювання всієї панелі
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(backgroundBrush, 0, 0, Width, Height);

            DrawBoard(g);
            DrawCoordinates(g);
        }


        // Малювання клітинок та фігур
        private void DrawBoard(Graphics g)
        {
            var board = ChessGame.Instance.Board;
            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(ChessGame.Instance.IsWhiteTurn);

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Brush cellBrush = (row + col) % 2 == 0 ? lightBrush : darkBrush;
                    DrawCell(g, col, row, cellBrush);

                    // Підсвітити короля, якщо шах
                    if (board[row, col] is King && board[row, col].IsWhite == ChessGame.Instance.IsWhiteTurn && isKingInCheck)
                    {
                        DrawCell(g, col, row, HighlightStyles.Check);
                    }

                    // Підсвітити останній хід
                    if (ChessGame.Instance.LastMove.HasValue)
                    {
                        var (fromPos, toPos) = ChessGame.Instance.LastMove.Value;
                        if ((row == fromPos.Y && col == fromPos.X) || (row == toPos.Y && col == toPos.X))
                        {
                            DrawCell(g, col, row, HighlightStyles.LastMove);
                        }
                    }

                    // Підсвітити можливі ходи
                    if (selectedPiece != null)
                    {
                        DrawValidMoves(g, row, col, board, isKingInCheck);
                    }

                    g.DrawRectangle(Pens.White, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);

                    // Малювання фігури
                    if (board[row, col] != null)
                    {
                        g.DrawImage(board[row, col].Icon, col * cellSize + CoordinateOffset + 5, row * cellSize + CoordinateOffset + 5, 50, 50);
                    }
                }
            }
        }

        private void DrawCell(Graphics g, int col, int row, Brush brush)
        {
            g.FillRectangle(brush, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
        }


        // Малювання можливих ходів
        private void DrawValidMoves(Graphics g, int row, int col, Piece[,] board, bool isKingInCheck)
        {
            Position startPos = selectedPiece;
            Position endPos = new Position(col, row);
            Piece piece = board[startPos.Y, startPos.X];

            if (piece == null || !piece.IsValidMove(endPos, board) ||
                (board[row, col] != null && board[row, col].IsWhite == piece.IsWhite))
            {
                return;
            }

            bool isKingMove = piece is King;
            bool isTargetSafe = !ChessGame.Instance.IsSquareUnderAttack(endPos, !piece.IsWhite);

            if (isKingMove)
            {
                Brush highlightBrush = board[row, col] != null && board[row, col].IsWhite != piece.IsWhite
                    ? HighlightStyles.CaptureMove
                    : HighlightStyles.ValidMove;
                DrawCell(g, col, row, highlightBrush);
            }
            else if (!isKingInCheck || ChessGame.Instance.IsMoveResolvingCheck(startPos, endPos, piece.IsWhite))
            {
                Brush highlightBrush = board[row, col] != null && board[row, col].IsWhite != piece.IsWhite
                    ? HighlightStyles.CaptureMove
                    : HighlightStyles.ValidMove;
                DrawCell(g, col, row, highlightBrush);
            }
        }


        // Малювання координат навколо дошки
        private void DrawCoordinates(Graphics g)
        {
            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                for (int col = 0; col < Size; col++)
                {
                    string letter = ((char)('A' + col)).ToString();
                    g.DrawString(letter, coordinateFont, textBrush, col * cellSize + CoordinateOffset + cellSize / 2 - 5, CoordinateOffset / 2 - 5);
                    g.DrawString(letter, coordinateFont, textBrush, col * cellSize + CoordinateOffset + cellSize / 2 - 5, Size * cellSize + CoordinateOffset + 5);
                }

                for (int row = 0; row < Size; row++)
                {
                    string number = (8 - row).ToString();
                    g.DrawString(number, coordinateFont, textBrush, CoordinateOffset / 2 - 10, row * cellSize + CoordinateOffset + cellSize / 2 - 5);
                    g.DrawString(number, coordinateFont, textBrush, Size * cellSize + CoordinateOffset + 5, row * cellSize + CoordinateOffset + cellSize / 2 - 5);
                }
            }
        }


        // Обробка кліку миші по дошці
        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (ChessGame.Instance.GameEnded)
                return;

            Position clickedPos = GetClickedPosition(e.X, e.Y);
            if (!IsValidPosition(clickedPos))
                return;

            var board = ChessGame.Instance.Board;

            if (selectedPiece == null)
            {
                SelectPiece(clickedPos, board);
            }
            else
            {
                TryMakeMove(selectedPiece, clickedPos, board);
                selectedPiece = null;
            }
        }


        // Отримання позиції, по якій клікнули
        private Position GetClickedPosition(int x, int y)
        {
            int clickedRow = (y - CoordinateOffset) / cellSize;
            int clickedCol = (x - CoordinateOffset) / cellSize;
            return new Position(clickedCol, clickedRow);
        }


        // Перевірка, чи координати у межах дошки
        private bool IsValidPosition(Position pos)
        {
            return pos.X >= 0 && pos.X < Size && pos.Y >= 0 && pos.Y < Size;
        }


        // Виділення фігури, якщо вона своя
        private void SelectPiece(Position pos, Piece[,] board)
        {
            if (board[pos.Y, pos.X] != null && board[pos.Y, pos.X].IsWhite == ChessGame.Instance.IsWhiteTurn)
            {
                selectedPiece = pos;
                Invalidate(); 
            }
        }


        // Спроба зробити хід
        private void TryMakeMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];

            if (piece == null || !IsValidMove(startPos, endPos, board))
                return;

            if (piece is King)
            {
                HandleKingMove(startPos, endPos, board);
            }
            else
            {
                HandleRegularMove(startPos, endPos, board);
            }
        }


        // Перевірка, чи хід допустимий
        private bool IsValidMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            return piece != null &&
                   piece.IsValidMove(endPos, board) &&
                   (board[endPos.Y, endPos.X] == null || board[endPos.Y, endPos.X].IsWhite != piece.IsWhite);
        }


        // Обробка ходу короля з перевіркою на безпеку
        private void HandleKingMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            Piece targetPiece = board[endPos.Y, endPos.X];

            board[startPos.Y, startPos.X] = null;
            board[endPos.Y, endPos.X] = piece;
            piece.UpdatePosition(endPos);
            Position oldKingPos = ChessGame.Instance.GetKingPosition(piece.IsWhite);
            ChessGame.Instance.UpdateKingPosition(endPos, piece.IsWhite);

            bool isTargetSafe = !ChessGame.Instance.IsSquareUnderAttack(endPos, !piece.IsWhite);

            board[startPos.Y, startPos.X] = piece;
            board[endPos.Y, endPos.X] = targetPiece;
            piece.UpdatePosition(startPos);
            ChessGame.Instance.UpdateKingPosition(oldKingPos, piece.IsWhite);

            if (isTargetSafe)
            {
                ExecuteMove(startPos, endPos, board);
            }
        }


        // Обробка звичайного ходу  
        private void HandleRegularMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(piece.IsWhite);


            // Реалізація ходу для фігур, що не є королем
            if (!isKingInCheck || ChessGame.Instance.IsMoveResolvingCheck(startPos, endPos, piece.IsWhite))
            {
                Piece capturedPiece = board[endPos.Y, endPos.X];
                board[endPos.Y, endPos.X] = piece;
                board[startPos.Y, startPos.X] = null;
                piece.UpdatePosition(endPos);

                Position kingPos = ChessGame.Instance.GetKingPosition(piece.IsWhite);
                bool wouldBeInCheck = ChessGame.Instance.IsSquareUnderAttack(kingPos, !piece.IsWhite);

                if (!wouldBeInCheck)
                {
                    ChessGame.Instance.LastMove = (startPos, endPos);
                    ChessGame.Instance.IsWhiteTurn = !ChessGame.Instance.IsWhiteTurn;
                    ChessGame.Instance.CheckGameState(this);
                }
                else
                {
                    board[startPos.Y, startPos.X] = piece;
                    board[endPos.Y, endPos.X] = capturedPiece;
                    piece.UpdatePosition(startPos);
                }
            }
        }


        // Метод, який виконує хід 
        private void ExecuteMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            board[endPos.Y, endPos.X] = piece;
            board[startPos.Y, startPos.X] = null;
            piece.UpdatePosition(endPos);

            if (piece is King)
            {
                ChessGame.Instance.UpdateKingPosition(endPos, piece.IsWhite);
            }

            ChessGame.Instance.LastMove = (startPos, endPos);
            ChessGame.Instance.IsWhiteTurn = !ChessGame.Instance.IsWhiteTurn;
            ChessGame.Instance.CheckGameState(this);
        }
    }
}
