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

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

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

        private void InitializeBoard()
        {
            BoardInitializer.Initialize(Board, _pieceFactory, out WhiteKingPos, out BlackKingPos);
        }

        public void RestartGame()
        {
            for (int row = 0; row < Size; row++)
                for (int col = 0; col < Size; col++)
                    Board[row, col] = null;

            Board.Initialize();
            InitializeBoard();
            IsWhiteTurn = true;
            LastMove = null;
            GameEnded = false;
            NotifyGameStateChanged();
        }

        public void UpdateKingPosition(Position pos, bool isWhite)
        {
            if (isWhite)
                WhiteKingPos.UpdatePosition(pos.X, pos.Y);
            else
                BlackKingPos.UpdatePosition(pos.X, pos.Y);
            NotifyGameStateChanged();
        }

        public Position GetKingPosition(bool isWhite) => isWhite ? WhiteKingPos : BlackKingPos;

        public bool IsSquareUnderAttack(Position targetPos, bool attackerIsWhite)
        {
            return BoardEvaluator.IsSquareUnderAttack(Board, targetPos, attackerIsWhite);
        }

        public bool IsKingInCheck(bool forWhite)
        {
            Position kingPos = GetKingPosition(forWhite);
            return IsSquareUnderAttack(kingPos, !forWhite);
        }

        public bool IsMoveResolvingCheck(Position startPos, Position endPos, bool forWhite)
        {
            return MoveValidator.IsMoveResolvingCheck(Board, startPos, endPos, forWhite, GetKingPosition(forWhite));
        }

        public bool HasLegalMoves(bool forWhite)
        {
            return MoveValidator.HasLegalMoves(Board, forWhite, GetKingPosition(forWhite));
        }

        public void CheckGameState(BoardPanel boardPanel)
        {
            Position kingPos = GetKingPosition(IsWhiteTurn);
            bool isInCheck = IsSquareUnderAttack(kingPos, !IsWhiteTurn);
            bool hasLegalMoves = HasLegalMoves(IsWhiteTurn);

            if (isInCheck && !hasLegalMoves)
                EndGame(!IsWhiteTurn ? "White wins by checkmate!" : "Black wins by checkmate!", boardPanel);
            else if (!isInCheck && !hasLegalMoves)
                EndGame("Stalemate! The game is a draw.", boardPanel);

            NotifyGameStateChanged();
        }

        public void EndGame(string message, BoardPanel boardPanel)
        {
            GameEnded = true;
            _logger?.LogGameResult(message);
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Would you like to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RestartGame();
                boardPanel.Invalidate();
                if (boardPanel.FindForm() is GameForm gameForm)
                    gameForm.ResetTimers();
            }
            NotifyGameStateChanged();
        }

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
