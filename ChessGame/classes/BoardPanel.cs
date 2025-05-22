using ChessGame.Classes.Pieces;
using ChessGame;
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
        private Brush lightBrush;
        private Brush darkBrush;
        private Brush backgroundBrush;
        public BoardPanel(int cellSize = 60, string background = "Default")
        {
            this.cellSize = cellSize;
            this.Width = cellSize * Size + CoordinateOffset * 2;
            this.Height = cellSize * Size + CoordinateOffset * 2;

            SetTheme(background);

            this.DoubleBuffered = true;
            this.Paint += Board_Paint;
            this.MouseClick += BoardPanel_MouseClick;
        }
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

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var board = ChessGame.Instance.Board;

            DrawBackground(g);
            DrawBoardSquares(g, board);
            DrawCoordinates(g);
            DrawPieces(g, board);
        }

        private void DrawBackground(Graphics g)
        {
            g.FillRectangle(backgroundBrush, 0, 0, Width, Height);
        }

        private void DrawBoardSquares(Graphics g, Piece[,] board)
        {
            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(ChessGame.Instance.IsWhiteTurn);

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Brush brush = GetSquareBrush(row, col, board, isKingInCheck);
                    g.FillRectangle(brush, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                    g.DrawRectangle(Pens.White, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                }
            }
        }

        private Brush GetSquareBrush(int row, int col, Piece[,] board, bool isKingInCheck)
        {
            if (IsKingInCheckSquare(row, col, board, isKingInCheck))
                return new SolidBrush(Color.FromArgb(255, 200, 0, 0));

            if (IsLastMoveSquare(row, col))
                return new SolidBrush(Color.FromArgb(255, 240, 240, 10));

            if (IsValidMoveSquare(row, col, board))
                return GetValidMoveBrush(row, col, board);

            return (row + col) % 2 == 0 ? lightBrush : darkBrush;
        }

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

        private void DrawPieces(Graphics g, Piece[,] board)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (board[row, col] != null)
                    {
                        g.DrawImage(board[row, col].Icon, col * cellSize + CoordinateOffset + 5, row * cellSize + CoordinateOffset + 5, 50, 50);
                    }
                }
            }
        }


        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (ChessGame.Instance.GameEnded) return;
            int clickedRow = (e.Y - CoordinateOffset) / cellSize;
            int clickedCol = (e.X - CoordinateOffset) / cellSize;

            if (clickedRow < 0 || clickedRow >= Size || clickedCol < 0 || clickedCol >= Size)
                return;

            var board = ChessGame.Instance.Board;

            if (selectedPiece == null)
            {
                if (board[clickedRow, clickedCol] != null && board[clickedRow, clickedCol].IsWhite == ChessGame.Instance.IsWhiteTurn)
                {
                    selectedPiece = new Position(clickedCol, clickedRow);
                    Invalidate();
                }
            }
            else
            {
                Position startPos = selectedPiece;
                Position endPos = new Position(clickedCol, clickedRow);
                Piece piece = board[startPos.Y, startPos.X];

                if (endPos.X >= 0 && endPos.X < Size && endPos.Y >= 0 && endPos.Y < Size &&
                    piece.IsValidMove(endPos, board) &&
                    (board[endPos.Y, endPos.X] == null || board[endPos.Y, endPos.X].IsWhite != piece.IsWhite))
                {
                    bool isKingMove = piece is King;
                    bool isKingInCheck = ChessGame.Instance.IsKingInCheck(piece.IsWhite);

                    if (isKingMove)
                    {
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
                            board[endPos.Y, endPos.X] = piece;
                            board[startPos.Y, startPos.X] = null;
                            piece.UpdatePosition(endPos);
                            ChessGame.Instance.UpdateKingPosition(endPos, piece.IsWhite);
                            ChessGame.Instance.LastMove = (startPos, endPos);
                            ChessGame.Instance.IsWhiteTurn = !ChessGame.Instance.IsWhiteTurn;
                            ChessGame.Instance.CheckGameState(this);
                        }
                    }
                    else
                    {
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
                                Invalidate();
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
                }

                selectedPiece = null;
                Invalidate();
            }
        }
    }
}