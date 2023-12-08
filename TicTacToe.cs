using System.Security.Cryptography.Xml;

namespace TicTacToe;

public partial class TicTacToe : Form
{
	public const int tableSize = 20;
	public const int elementsInRowToWin = 5;
	internal TicTacToeCell[,] table = new TicTacToeCell[tableSize, tableSize];
	CellContent currentPlayer = CellContent.Circle;
	bool currentPlayerWon = false;
	int scoreCrosses = 0;
	int scoreCircles = 0;
	Bitmap currentPlayerCrosses;
	Bitmap currentPlayerCircles;
	public TicTacToe()
	{
#warning texty, vrstvy zobrazovani, viteznou hlasku, 
		InitializeComponent();
		SetControlTheme(this);
		SetColorTheme(this);
		CreateTableArray(table);
		WindowState = FormWindowState.Maximized;
		ConfigureUI();
		Bitmap bitmap = new Bitmap(tableScreen.Width, tableScreen.Height);// (Bitmap)tableScreen.Image
		TicTacToeDrawer.DrawTable(bitmap, table);
		tableScreen.Image = bitmap;
		SetSubscriptions();
	}

	private void SetSubscriptions()
	{
		tableScreen.MouseDown += TicTacToe_MouseDown;
		buttonRestart.Click += ButtonRestart_Click;
	}

	private void ButtonRestart_Click(object? sender, EventArgs e)
	{
		currentPlayerWon = false;
		SwitchCurrentPlayer();
		CreateTableArray(table);
		DrawTable();
	}

	private void TicTacToe_MouseDown(object? sender, MouseEventArgs e)
	{
		TicTacToeCell? cell = FindHitCell(e.X, e.Y);
		if (cell is null || currentPlayerWon) return;

		HandleCellSelect(cell);
	}

	private void HandleCellSelect(TicTacToeCell cell)
	{
		if (cell.cellContent != CellContent.None) return;

		cell.cellContent = currentPlayer;

		currentPlayerWon = TryFindWinnerHorinzontally(currentPlayer) |
			 TryFindWinnerVertically(currentPlayer) |
			 TryFindWinnerFromTopLeft(currentPlayer);
		if (currentPlayerWon)
		{
			if (currentPlayer == CellContent.Cross) scoreCrosses++;
			else scoreCircles++;
			CurrentPlayerWon();
		}
		else SwitchCurrentPlayer();


		DrawTable();
	}

	private void SwitchCurrentPlayer()
	{
		currentPlayer =
		currentPlayer == CellContent.Cross ?
		CellContent.Circle :
		CellContent.Cross;
		currentPlayePictureBox.Image =
			currentPlayer == CellContent.Cross ?
			currentPlayerCrosses :
			currentPlayerCircles;
	}

	private void CurrentPlayerWon()
	{
		MessageBox.Show("Player won");
		scoreCirclesText.Text = scoreCircles.ToString();
		scoreCrossesText.Text = scoreCrosses.ToString();
	}

	private void DrawTable()
	{
		Bitmap bitmap = (Bitmap)tableScreen.Image;
		TicTacToeDrawer.ClearScreen(bitmap, Color.Black);
		TicTacToeDrawer.DrawTable(bitmap, table);
		tableScreen.Image = bitmap;
	}

	private TicTacToeCell? FindHitCell(int mouseX, int mouseY)
	{
		Point mousePoint = new Point(mouseX, mouseY);
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				TicTacToeCell cell = table[i, j];
				if (cell.DoesPointBelongWithinCell(mousePoint, TicTacToeDrawer.cellWidth))
				{
					return cell;
				}
			}
		}
		return null;
	}

	private void ConfigureUI()
	{
		Point anchor = new Point(0, 0);
		int cellWidth = 170;
		Bitmap bitmap = new Bitmap(scoreCirclesPictureBox.Width, scoreCirclesPictureBox.Height);
		TicTacToeDrawer.DrawCircle(
			Graphics.FromImage(bitmap),
			anchor,
			cellWidth
			);
		TicTacToeDrawer.DrawBoundary(bitmap);
		scoreCirclesPictureBox.Image = bitmap;

		bitmap = new Bitmap(scoreCrossesPictureBox.Width, scoreCrossesPictureBox.Height);
		TicTacToeDrawer.DrawCross(
			Graphics.FromImage(bitmap),
			anchor,
			cellWidth
			);
		TicTacToeDrawer.DrawBoundary(bitmap);
		scoreCrossesPictureBox.Image = bitmap;


		currentPlayerCircles = new Bitmap(currentPlayePictureBox.Width, currentPlayePictureBox.Height);
		TicTacToeDrawer.DrawBoundary(currentPlayerCircles);
		TicTacToeDrawer.DrawCircle(
			Graphics.FromImage(currentPlayerCircles),
			anchor,
			currentPlayePictureBox.Width
			);

		currentPlayerCrosses = new Bitmap(currentPlayePictureBox.Width, currentPlayePictureBox.Height);
		TicTacToeDrawer.DrawBoundary(currentPlayerCrosses);
		TicTacToeDrawer.DrawCross(
			Graphics.FromImage(currentPlayerCrosses),
			anchor,
			currentPlayePictureBox.Width
			);

		SwitchCurrentPlayer();
	}

	private static void CreateTableArray(TicTacToeCell[,] table)
	{
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				table[i, j] = new TicTacToeCell(new Point(i, j));
			}
		}
	}

	private static void SetColorTheme(Control parent)
	{
		foreach (Control control in parent.Controls)
		{
			SetControlTheme(control);
			if (control.HasChildren) SetColorTheme(control);
		}
	}

	private static void SetControlTheme(Control control)
	{
		control.BackColor = Color.Black;
		control.ForeColor = Color.White;
	}

	private bool TryFindWinnerHorinzontally(CellContent shape)
	{
		int shapeCount = 0;
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				TicTacToeCell cell = table[j, i];
				shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
				if (shapeCount == elementsInRowToWin) return true;
			}
			shapeCount = 0;
		}

		return false;
	}

	private bool TryFindWinnerVertically(CellContent shape)
	{
		int shapeCount = 0;
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				TicTacToeCell cell = table[i, j];
				shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
				if (shapeCount == elementsInRowToWin) return true;
			}
			shapeCount = 0;
		}

		return false;
	}

	private bool TryFindWinnerFromTopLeft(CellContent shape)
	{
		// Check diagonals from top-left to bottom-right
		for (int startCol = 0; startCol <= tableSize - elementsInRowToWin; startCol++)
		{
			for (int startRow = 0; startRow <= tableSize - elementsInRowToWin; startRow++)
			{
				int shapeCount = 0;
				for (int i = 0; i < elementsInRowToWin; i++)
				{
					TicTacToeCell cell = table[startRow + i, startCol + i];
					shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
					if (shapeCount == elementsInRowToWin) return true;
				}
			}
		}

		// Check diagonals from top-right to bottom-left
		for (int startCol = elementsInRowToWin - 1; startCol < tableSize; startCol++)
		{
			for (int startRow = 0; startRow <= tableSize - elementsInRowToWin; startRow++)
			{
				int shapeCount = 0;
				for (int i = 0; i < elementsInRowToWin; i++)
				{
					TicTacToeCell cell = table[startRow + i, startCol - i];
					shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
					if (shapeCount == elementsInRowToWin) return true;
				}
			}
		}

		return false;
	}
#if no
	private bool TryFindWinnerFromTopLeft(CellContent shape)
	{
		int shapeCount = 0;
		int offset = 0;
		const int diagonalTableSize = 29;
		for (int i = diagonalTableSize - 1; i >= 0; i--)
		{
			for (int j = 0; j < diagonalTableSize; j++)
			{
				int xOffset = j + offset;
				int yOffset = i - offset;
				if (xOffset < 0				|| 
					yOffset < 0				||
					xOffset >= tableSize	|| 
					yOffset >= tableSize)
				{
					offset = 0;
					break;
				}
				TicTacToeCell cell = table[xOffset, yOffset];
				shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
				if (shapeCount == elementsInRowToWin) return true;

			}
			shapeCount = 0;

		}

		return false;
	}
#endif
}