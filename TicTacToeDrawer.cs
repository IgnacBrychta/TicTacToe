using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe;

internal static class TicTacToeDrawer
{
	public const int cellWidth = 68;
	private static Color crossColor = Color.Red;
	private static Color circleColor = Color.Blue;
	public const float penWidth = 10f;
	private static Color cellBorderColor = Color.White;
	private static Pen crossPen = new Pen(crossColor, penWidth);
	private static Pen cellBorderPen = new Pen(cellBorderColor, penWidth);
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
			anchor.X * cellWidth, 
			anchor.Y * cellWidth,
			cellWidth, 
			cellWidth
			);
	}

	internal static void DrawCircle(Graphics g, Point anchor, int cellWidth)
	{
		g.DrawEllipse(
			circlePen,
			anchor.X,
			anchor.Y,
			anchor.X + cellWidth,
			anchor.Y = cellWidth
			);
	}

	private static void DrawCross(Graphics g, Point anchor)
	{
		g.DrawLine(
			crossPen, 
			new Point(anchor.X * cellWidth, anchor.Y  * cellWidth), 
			new Point((anchor.X + 1) * cellWidth, (anchor.Y + 1) * cellWidth)
			);
		g.DrawLine(
			crossPen,
			new Point(anchor.X * cellWidth, (anchor.Y + 1) * cellWidth),
			new Point((anchor.X + 1) * cellWidth, anchor.Y * cellWidth)
			);
	}

	internal static void DrawCross(Graphics g, Point anchor, int cellWidth)
	{
		g.DrawLine(
			crossPen,
			anchor,
			new Point(anchor.X + cellWidth, anchor.Y + cellWidth)
			);
		g.DrawLine(
			crossPen,
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
