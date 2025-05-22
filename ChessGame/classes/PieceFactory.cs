using System.Drawing;

namespace ChessGame.Classes.Pieces
{
    public class PieceFactory
    {
        private readonly Dictionary<(PieceType, bool), string> _imagePaths;

        public PieceFactory()
        {
            _imagePaths = new Dictionary<(PieceType, bool), string>
            {
                {(PieceType.King, true), "assets/WhiteKing.png"},
                {(PieceType.King, false), "assets/BlackKing.png"},
                {(PieceType.Knight, true), "assets/WhiteKnight.png"},
                {(PieceType.Knight, false), "assets/BlackKnight.png"},
                {(PieceType.Pawn, true), "assets/WhitePawn.png"},
                {(PieceType.Pawn, false), "assets/BlackPawn.png"},
                {(PieceType.Queen, true), "assets/WhiteQueen.png"},
                {(PieceType.Queen, false), "assets/BlackQueen.png"},
                {(PieceType.Rook, true), "assets/WhiteRook.png"},
                {(PieceType.Rook, false), "assets/BlackRook.png"},
                {(PieceType.Bishop, true), "assets/WhiteBishop.png"},
                {(PieceType.Bishop, false), "assets/BlackBishop.png"}
            };
        }
        public Piece CreatePiece(PieceType type, Position position, bool isWhite)
        {
            if (!_imagePaths.TryGetValue((type, isWhite), out string imagePath))
            {
                throw new ArgumentException($"No image for the shape {type} color {(isWhite ? "white" : "black")}");
            }

            Image icon = Image.FromFile(imagePath);

            return type switch
            {
                PieceType.King => new King(position, isWhite, icon),
                PieceType.Knight => new Knight(position, isWhite, icon),
                PieceType.Pawn => new Pawn(position, isWhite, icon),
                PieceType.Queen => new Queen(position, isWhite, icon),
                PieceType.Rook => new Rook(position, isWhite, icon),
                PieceType.Bishop => new Bishop(position, isWhite, icon),
                _ => throw new ArgumentException("Unknown shape type")
            };
        }
    }
}