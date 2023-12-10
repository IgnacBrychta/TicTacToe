namespace TicTacToe;

internal static class TicTacToeDrawer
{
	public const int cellWidth = 68;
	public static Color crossColor = Color.Red;
	public static Color circleColor = Color.Blue;
	public const float penWidth = 10f;
	public const int sizedPenWidth = (int)penWidth / 2 + 3;
	private static Color cellBorderColor = Color.White;
	private static Color highlightedBorderColor = Color.Yellow;
	private static Color mouseoverPenColor = Color.Green;
	private static Pen crossPen = new Pen(crossColor, penWidth);
	private static Pen cellBorderPen = new Pen(cellBorderColor, penWidth);
	private static Pen highlightedCellBorderPen = new Pen(highlightedBorderColor, penWidth);
	private static Pen mouseoverCellBorderPen = new Pen(mouseoverPenColor, penWidth);
	private static Pen circlePen = new Pen(circleColor, penWidth);
	/// <summary>
	/// Draws a Tic Tac Toe table on a <see cref="Bitmap"/>, but does not clear it beforehead. Use <see cref="ClearScreen(Bitmap, Color)"/> for that instead.
	/// </summary>
	/// <param name="bitmap"></param>
	/// <param name="table"></param>
	internal static void DrawTable(Bitmap bitmap, TicTacToeCell[,] table)
	{
		Graphics g = Graphics.FromImage(bitmap);
		for (int i = 0; i < TicTacToe.tableSize; i++)
		{
			for (int j = 0; j < TicTacToe.tableSize; j++)
			{
				TicTacToeCell cell = table[i, j];
				DrawTableCell(cell, g);
			}
		}

		DrawHighlightedCells(g, table);
	}

	public static void DrawHighlightedCells(Graphics g, TicTacToeCell[,] table)
	{
		for (int i = 0; i < TicTacToe.tableSize; i++)
		{
			for (int j = 0; j < TicTacToe.tableSize; j++)
			{
				TicTacToeCell cell = table[i, j];
				if (!cell.highlight) continue;

				g.DrawRectangle(
					highlightedCellBorderPen,
					cell.anchor.X * cellWidth,
					cell.anchor.Y * cellWidth,
					cellWidth,
					cellWidth
					);
			}
		}
	}

	public static void HighlightSingleCell(TicTacToeCell cell, Graphics g)
	{
		g.DrawRectangle(
			mouseoverCellBorderPen,
			cell.anchor.X * cellWidth,
			cell.anchor.Y * cellWidth,
			cellWidth,
			cellWidth
			);
	}

	private static void DrawTableCell(TicTacToeCell cell, Graphics g)
	{
		g.DrawRectangle(
			cellBorderPen,
			cell.anchor.X * cellWidth,
			cell.anchor.Y * cellWidth,
			cellWidth,
			cellWidth
			);

		if (cell.cellContent == CellContent.Cross)
			DrawCross(g, cell.anchor);
		else if (cell.cellContent == CellContent.Circle)
			DrawCircle(g, cell.anchor);
	}

	public static void DrawBoundary(Bitmap bitmap)
	{
		Graphics g = Graphics.FromImage(bitmap);
		g.DrawRectangle(
			cellBorderPen,
			0,
			0,
			bitmap.Width - 1,
			bitmap.Height - 1
			);
	}

	private static void DrawCircle(Graphics g, Point anchor)
	{
		g.DrawEllipse(
			circlePen,
			anchor.X * cellWidth + sizedPenWidth, 
			anchor.Y * cellWidth + sizedPenWidth,
			cellWidth - 2 * sizedPenWidth, 
			cellWidth - 2 * sizedPenWidth
			);
	}

	internal static void DrawCircle(Graphics g, Point anchor, int cellWidth, Pen? pen = null)
	{
		g.DrawEllipse(
			pen ?? circlePen,
			anchor.X,
			anchor.Y,
			anchor.X + cellWidth,
			anchor.Y + cellWidth
			);
	}

	private static void DrawCross(Graphics g, Point anchor)
	{
		g.DrawLine(
			crossPen, 
			new Point(
					anchor.X * cellWidth + sizedPenWidth,
					anchor.Y * cellWidth + sizedPenWidth
				), 
			new Point(
					(anchor.X + 1) * cellWidth - sizedPenWidth, 
					(anchor.Y + 1) * cellWidth - sizedPenWidth
				)
			);
		g.DrawLine(
			crossPen,
			new Point(
					anchor.X * cellWidth + sizedPenWidth,
					(anchor.Y + 1) * cellWidth - sizedPenWidth
				),
			new Point(
					(anchor.X + 1) * cellWidth - sizedPenWidth,
					anchor.Y * cellWidth + sizedPenWidth
				)
			);
	}

	internal static void DrawCross(Graphics g, Point anchor, int cellWidth, Pen? pen = null)
	{
		g.DrawLine(
			pen ?? crossPen,
			anchor,
			new Point(anchor.X + cellWidth, anchor.Y + cellWidth)
			);
		g.DrawLine(
			pen ?? crossPen,
			new Point(anchor.X, anchor.Y + cellWidth),
			new Point(anchor.X + cellWidth, anchor.Y)
			);
	}

	public static void ClearScreen(Bitmap bitmap, Color color)
	{
		Graphics g = Graphics.FromImage(bitmap);
		g.Clear(color);
	}
}
