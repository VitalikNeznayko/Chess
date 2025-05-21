using ChessGame.Classes;
using ChessGame.Classes.Pieces;
using System.Xml.Linq;

public class Knight : Piece
{
	public Knight(Position position, bool isWhite, Image icon) : base(position, isWhite)
	{
		Name = "Knight";
        Icon = icon;
    }

	public override bool IsValidMove(Position endPos, Piece[,] board, bool isCheckEvaluation = false)
	{
		int deltaCol = Math.Abs(endPos.X - Position.X);
		int deltaRow = Math.Abs(endPos.Y - Position.Y);
		return (deltaCol == 2 && deltaRow == 1) || (deltaCol == 1 && deltaRow == 2);
	}
}