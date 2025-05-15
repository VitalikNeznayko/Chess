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
        private readonly Brush backgroundBrush = new SolidBrush(Color.FromArgb(0xE8, 0xED, 0xF9));
       
        public BoardPanel(int cellSize = 60)
        {
            this.cellSize = cellSize;
            this.Width = cellSize * Size + CoordinateOffset * 2;
            this.Height = cellSize * Size + CoordinateOffset * 2;
            this.DoubleBuffered = true;
            this.Paint += Board_Paint;
            this.MouseClick += BoardPanel_MouseClick;
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var board = ChessGame.Instance.Board;

            bool isKingInCheck = ChessGame.Instance.IsKingInCheck(ChessGame.Instance.IsWhiteTurn);
            g.FillRectangle(backgroundBrush, 0, 0, this.Width, this.Height);
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Brush White = new SolidBrush(Color.FromArgb(0xE8, 0xED, 0xF9));
                    Brush Black = new SolidBrush(Color.FromArgb(0xB7, 0xC0, 0xD8));

                    Brush brush = (row + col) % 2 == 0 ? White : Black;
                    g.FillRectangle(brush, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);

                    if (board[row, col] is King && board[row, col].IsWhite == ChessGame.Instance.IsWhiteTurn && isKingInCheck)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 200, 0, 0)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                    }

                    if (ChessGame.Instance.LastMove.HasValue)
                    {
                        var (fromPos, toPos) = ChessGame.Instance.LastMove.Value;
                        if ((row == fromPos.Y && col == fromPos.X) || (row == toPos.Y && col == toPos.X))
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 240, 240, 10)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                        }
                    }

                    if (selectedPiece != null)
                    {
                        Position startPos = selectedPiece;
                        Position endPos = new Position(col, row);
                        Piece piece = board[startPos.Y, startPos.X];

                        if (piece != null && piece.IsValidMove(endPos, board) && (board[row, col] == null || board[row, col].IsWhite != piece.IsWhite))
                        {
                            bool isKingMove = piece is King;
                            bool isTargetSafe = !ChessGame.Instance.IsSquareUnderAttack(endPos, !piece.IsWhite);

                            if (isKingMove)
                            {
                                if (board[row, col] != null && board[row, col].IsWhite != piece.IsWhite)
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 50, 180, 50)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                                }
                                else
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 185, 155, 255)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                                }
                            }
                            else
                            {
                                if (!isKingInCheck || ChessGame.Instance.IsMoveResolvingCheck(startPos, endPos, piece.IsWhite))
                                {
                                    if (board[row, col] != null && board[row, col].IsWhite != piece.IsWhite)
                                    {
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 50, 180, 50)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                                    }
                                    else
                                    {
                                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 185, 155, 255)), col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);
                                    }
                                }
                            }
                        }
                    }

                    g.DrawRectangle(Pens.White, col * cellSize + CoordinateOffset, row * cellSize + CoordinateOffset, cellSize, cellSize);

                    if (board[row, col] != null)
                    {
                        g.DrawImage(board[row, col].Icon, col * cellSize + CoordinateOffset + 5, row * cellSize + CoordinateOffset + 5, 50, 50);
                    }
                }
            }

            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                for (int col = 0; col < Size; col++)
                {
                    string letter = ((char)('a' + col)).ToString();
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