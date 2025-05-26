using Chess.Classes.Pieces;
using Chess.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class BoardInitializer
    {
        public static void Initialize(Piece[,] board, PieceFactory factory, out Position whiteKingPos, out Position blackKingPos)
        {
            for (int col = 0; col < 8; col++)
            {
                board[1, col] = factory.CreatePiece(PieceType.Pawn, new Position(col, 1), false);
                board[6, col] = factory.CreatePiece(PieceType.Pawn, new Position(col, 6), true);
            }

            board[0, 0] = factory.CreatePiece(PieceType.Rook, new Position(0, 0), false);
            board[0, 1] = factory.CreatePiece(PieceType.Knight, new Position(1, 0), false);
            board[0, 2] = factory.CreatePiece(PieceType.Bishop, new Position(2, 0), false);
            board[0, 3] = factory.CreatePiece(PieceType.Queen, new Position(3, 0), false);
            board[0, 4] = factory.CreatePiece(PieceType.King, new Position(4, 0), false);
            board[0, 5] = factory.CreatePiece(PieceType.Bishop, new Position(5, 0), false);
            board[0, 6] = factory.CreatePiece(PieceType.Knight, new Position(6, 0), false);
            board[0, 7] = factory.CreatePiece(PieceType.Rook, new Position(7, 0), false);

            board[7, 0] = factory.CreatePiece(PieceType.Rook, new Position(0, 7), true);
            board[7, 1] = factory.CreatePiece(PieceType.Knight, new Position(1, 7), true);
            board[7, 2] = factory.CreatePiece(PieceType.Bishop, new Position(2, 7), true);
            board[7, 3] = factory.CreatePiece(PieceType.Queen, new Position(3, 7), true);
            board[7, 4] = factory.CreatePiece(PieceType.King, new Position(4, 7), true);
            board[7, 5] = factory.CreatePiece(PieceType.Bishop, new Position(5, 7), true);
            board[7, 6] = factory.CreatePiece(PieceType.Knight, new Position(6, 7), true);
            board[7, 7] = factory.CreatePiece(PieceType.Rook, new Position(7, 7), true);

            whiteKingPos = new Position(4, 7);
            blackKingPos = new Position(4, 0);
        }
    }
}
