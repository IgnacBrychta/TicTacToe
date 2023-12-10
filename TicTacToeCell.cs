namespace TicTacToe;

internal class TicTacToeCell
{
	internal Point anchor;
	internal CellContent cellContent;
	internal bool highlight = false;

	internal TicTacToeCell(Point anchor)
	{
		this.anchor = anchor;
		cellContent = CellContent.None;
	}

	public override string ToString()
	{
		return $"{anchor}: {cellContent}";
	}

	public bool DoesPointBelongWithinCell(Point point, int cellWidth)
	{
		return
			anchor.X * cellWidth < point.X &&
			anchor.Y * cellWidth < point.Y &&
			(anchor.X + 1) * cellWidth > point.X &&
			(anchor.Y + 1) * cellWidth > point.Y;
	}

}

internal enum CellContent
{
	None,
	Cross,
	Circle
}
