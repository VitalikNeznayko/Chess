using ChessGame.Classes.Pieces;
using ChessGame.Classes;
using System;
using System.Windows.Forms;
using System.Timers;

namespace ChessGame
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

        private ChessGame()
        {
            Board = new Piece[Size, Size];
            IsWhiteTurn = true;
            LastMove = null;
            GameEnded = false;
            _pieceFactory = new PieceFactory();
            InitializeBoard();
        }

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

        }

        public static ChessGame Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ChessGame();
				return _instance;
			}

		}

		private void InitializeBoard()
		{
            for (int col = 0; col < Size; col++)
            {
                Board[1, col] = _pieceFactory.CreatePiece(PieceType.Pawn, new Position(col, 1), false);
                Board[6, col] = _pieceFactory.CreatePiece(PieceType.Pawn, new Position(col, 6), true);
            }

            Board[0, 0] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(0, 0), false);
            Board[0, 1] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(1, 0), false);
            Board[0, 2] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(2, 0), false);
            Board[0, 3] = _pieceFactory.CreatePiece(PieceType.Queen, new Position(3, 0), false);
            Board[0, 4] = _pieceFactory.CreatePiece(PieceType.King, new Position(4, 0), false);
            Board[0, 5] = _pieceFactory.CreatePiece(PieceType.Bishop, new Position(5, 0), false);
            Board[0, 6] = _pieceFactory.CreatePiece(PieceType.Knight, new Position(6, 0), false);
            Board[0, 7] = _pieceFactory.CreatePiece(PieceType.Rook, new Position(7, 0), false);

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


		public void UpdateKingPosition(Position pos, bool isWhite)
		{
			if (isWhite)
				WhiteKingPos.UpdatePosition(pos.X, pos.Y);
			else
				BlackKingPos.UpdatePosition(pos.X, pos.Y);
		}

		public Position GetKingPosition(bool isWhite)
		{
			return isWhite ? WhiteKingPos : BlackKingPos;
		}

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

		public bool IsKingInCheck(bool forWhite)
		{
			Position kingPos = GetKingPosition(forWhite);
			return IsSquareUnderAttack(kingPos, !forWhite);
		}

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

			Board[startPos.Y, startPos.X] = piece;
			Board[endPos.Y, endPos.X] = capturedPiece;

			return !stillInCheck;
		}

		public bool HasLegalMoves(bool forWhite)
		{
			for (int startRow = 0; startRow < Size; startRow++)
			{
				for (int startCol = 0; startCol < Size; startCol++)
				{
					Piece piece = Board[startRow, startCol];
					if (piece != null && piece.IsWhite == forWhite)
					{
						for (int targetRow = 0; targetRow < Size; targetRow++)
						{
							for (int targetCol = 0; targetCol < Size; targetCol++)
							{
								Position targetPos = new Position(targetCol, targetRow);
								if (piece.IsValidMove(targetPos, Board) &&
									(Board[targetRow, targetCol] == null || Board[targetRow, targetCol].IsWhite != piece.IsWhite))
								{
									Piece tempPiece = Board[targetRow, targetCol];
									Board[targetRow, targetCol] = piece;
									Board[startRow, startCol] = null;

									Position originalKingPos = GetKingPosition(forWhite);
									Position kingPosToCheck = piece is King ? targetPos : originalKingPos;

									bool wouldBeInCheck = IsSquareUnderAttack(kingPosToCheck, !forWhite);

									Board[startRow, startCol] = piece;
									Board[targetRow, targetCol] = tempPiece;

									if (!wouldBeInCheck)
									{
										return true;
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

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
		}
        public void EndGame(string message, BoardPanel boardPanel)
        {
            GameEnded = true;
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (MessageBox.Show("Would you like to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RestartGame();
				boardPanel.Invalidate();
                if (boardPanel.FindForm() is GameForm gameForm)
                {
                    gameForm.ResetTimers();
                }
                else
                {
                    MessageBox.Show("boardPanel.Parent is not GameForm");
                }
            }
        }
    }
}