using Chess.Classes.Pieces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Chess.Classes;


namespace Chess.Classes
{
    public class BoardPanel : Panel
    {
        private const int Size = 8;
        private int cellSize = 60;
        private Position selectedPiece = null;
        private const int CoordinateOffset = 35;
        private readonly Font coordinateFont = new Font("Arial", 12, FontStyle.Bold);


        // Brushes for cell filling
        private Brush lightBrush;
        private Brush darkBrush;
        private Brush backgroundBrush;

        // Colors for highlighting
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

        // Set the board's color theme
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

        // Handle game state change event
        private void OnGameStateChanged(object sender, ChessGame.GameStateChangedEventArgs e)
        {
            Invalidate();
        }

        // Paint the entire panel
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(backgroundBrush, 0, 0, Width, Height);
            DrawBoard(g);
            DrawCoordinates(g);
        }

        // Simplified method to draw the board
        private void DrawBoard(Graphics g)
        {
            var board = ChessGame.Instance.Board;
            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(ChessGame.Instance.IsWhiteTurn);

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    // Determine the brush for the cell (base color or highlight)
                    Brush cellBrush = GetCellHighlightBrush(row, col, board, isKingInCheck);
                    DrawCell(g, col, row, cellBrush);

                    // Draw cell border
                    g.DrawRectangle(Pens.White, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);

                    // Draw piece if present
                    if (board[row, col] != null)
                    {
                        g.DrawImage(board[row, col].Icon, col * cellSize + CoordinateOffset + 5, row * cellSize + CoordinateOffset + 5, 50, 50);
                    }
                }
            }
        }

        // Determine the highlight brush for a cell
        private Brush GetCellHighlightBrush(int row, int col, Piece[,] board, bool isKingInCheck)
        {
            // Default cell color
            Brush cellBrush = (row + col) % 2 == 0 ? lightBrush : darkBrush;

            // Highlight king in check
            if (board[row, col] is King && board[row, col].IsWhite == ChessGame.Instance.IsWhiteTurn && isKingInCheck)
            {
                return HighlightStyles.Check;
            }

            // Highlight last move
            if (ChessGame.Instance.LastMove.HasValue)
            {
                var (fromPos, toPos) = ChessGame.Instance.LastMove.Value;
                if ((row == fromPos.Y && col == fromPos.X) || (row == toPos.Y && col == toPos.X))
                {
                    return HighlightStyles.LastMove;
                }
            }

            // Highlight valid moves for selected piece
            if (selectedPiece != null)
            {
                Position startPos = selectedPiece;
                Position endPos = new Position(col, row);
                Piece piece = board[startPos.Y, startPos.X];

                if (piece != null && piece.IsValidMove(endPos, board) &&
                    (board[row, col] == null || board[row, col].IsWhite != piece.IsWhite))
                {
                    bool isKingMove = piece is King;
                    bool isTargetSafe = !ChessGame.Instance.IsSquareUnderAttack(endPos, !piece.IsWhite);

                    if (isKingMove && isTargetSafe)
                    {
                        return board[row, col] != null && board[row, col].IsWhite != piece.IsWhite
                            ? HighlightStyles.CaptureMove
                            : HighlightStyles.ValidMove;
                    }
                    else if (!isKingInCheck || ChessGame.Instance.IsMoveResolvingCheck(startPos, endPos, piece.IsWhite))
                    {
                        return board[row, col] != null && board[row, col].IsWhite != piece.IsWhite
                            ? HighlightStyles.CaptureMove
                            : HighlightStyles.ValidMove;
                    }
                }
            }

            return cellBrush;
        }

        // Draw a single cell
        private void DrawCell(Graphics g, int col, int row, Brush brush)
        {
            g.FillRectangle(brush, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
        }

        // Draw coordinates around the board
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

        // Handle mouse click on the board
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

        // Get the clicked position
        private Position GetClickedPosition(int x, int y)
        {
            int clickedRow = (y - CoordinateOffset) / cellSize;
            int clickedCol = (x - CoordinateOffset) / cellSize;
            return new Position(clickedCol, clickedRow);
        }

        // Check if the position is within the board
        private bool IsValidPosition(Position pos)
        {
            return pos.X >= 0 && pos.X < Size && pos.Y >= 0 && pos.Y < Size;
        }

        // Select a piece if it belongs to the current player
        private void SelectPiece(Position pos, Piece[,] board)
        {
            if (board[pos.Y, pos.X] != null && board[pos.Y, pos.X].IsWhite == ChessGame.Instance.IsWhiteTurn)
            {
                selectedPiece = pos;
                Invalidate();
            }
        }

        // Attempt to make a move
        private void TryMakeMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];

            if (piece == null || !IsValidMove(startPos, endPos, board))
                return;

            if (piece is King)
            {
                if (ChessGame.Instance.IsCastlingMove(startPos, endPos, board))
                {
                    ChessGame.Instance.PerformCastling(startPos, endPos, board);
                    Invalidate();
                    return;
                }

                HandleKingMove(startPos, endPos, board);
            }
            else
            {
                HandleRegularMove(startPos, endPos, board);
            }
        }

        // Check if a move is valid
        private bool IsValidMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            return piece != null &&
                   piece.IsValidMove(endPos, board) &&
                   (board[endPos.Y, endPos.X] == null || board[endPos.Y, endPos.X].IsWhite != piece.IsWhite);
        }

        // Handle king moves with safety checks
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

        // Handle regular moves
        private void HandleRegularMove(Position startPos, Position endPos, Piece[,] board)
        {
            Piece piece = board[startPos.Y, startPos.X];
            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(piece.IsWhite);

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

        // Execute a move
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

            if (piece is King k) k.HasMoved = true;
            if (piece is Rook r) r.HasMoved = true;
        }
    }
}

