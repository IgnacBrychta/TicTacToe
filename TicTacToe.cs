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
	Bitmap? currentPlayerCrosses;
	Bitmap? currentPlayerCircles;
	public TicTacToe()
	{
#warning vrstvy zobrazovani 
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
		Icon = new Icon("../../../resources/icon.ico");
		new AdvertisementController("../../../resources", sponsoredPictureBox) { RefreshDelay = 6000 }.Start();
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

		(bool horizontalWin, List<TicTacToeCell>? cellsHorizontal) = TryFindWinnerHorinzontally(currentPlayer);
		(bool verticalWin, List<TicTacToeCell>? cellsVertical) = TryFindWinnerVertically(currentPlayer);
		(bool diagonalWin, List<TicTacToeCell>? cellsDiagonal) = TryFindWinnerFromTopLeft(currentPlayer);
		currentPlayerWon = horizontalWin || verticalWin || diagonalWin;

		if (currentPlayerWon)
		{
			if (currentPlayer == CellContent.Cross) scoreCrosses++;
			else scoreCircles++;

			// must be AddRange() and not winningCells = cellsHorizontal - 
			// the player might win in more than one direction
			List<TicTacToeCell> winningCells = new();
			if (horizontalWin) winningCells.AddRange(cellsHorizontal!);
			if (verticalWin) winningCells.AddRange(cellsVertical!);
			if (diagonalWin) winningCells.AddRange(cellsDiagonal!);

			// highlighted (winning) cells are shown with a different color
			winningCells.ForEach(cell => cell.highlight = true);

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
		MessageBox.Show(
			"Je to prostì borec. GG.",
			$"{(currentPlayer == CellContent.Cross ? "Køížky" : "Koleèka")} vyhrávají!",
			MessageBoxButtons.OK,
			MessageBoxIcon.Information
			);
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
		int cellWidth = scoreCirclesPictureBox.Width;// 170;
		const float penWidth = 40f;
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



		const int offset = 3;

		currentPlayerCircles = new Bitmap(currentPlayePictureBox.Width, currentPlayePictureBox.Height);
		TicTacToeDrawer.DrawBoundary(currentPlayerCircles);
		TicTacToeDrawer.DrawCircle(
			Graphics.FromImage(currentPlayerCircles),
			anchor,
			currentPlayePictureBox.Width - offset,
			new Pen(TicTacToeDrawer.circleColor, penWidth)
			);

		currentPlayerCrosses = new Bitmap(currentPlayePictureBox.Width, currentPlayePictureBox.Height);
		TicTacToeDrawer.DrawBoundary(currentPlayerCrosses);
		TicTacToeDrawer.DrawCross(
			Graphics.FromImage(currentPlayerCrosses),
			anchor,
			currentPlayePictureBox.Width - offset,
			new Pen(TicTacToeDrawer.crossColor, penWidth)
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

	private (bool, List<TicTacToeCell>?) TryFindWinnerHorinzontally(CellContent shape)
	{
		int shapeCount = 0;
		List<TicTacToeCell> cells = new();
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				TicTacToeCell cell = table[j, i];
				if (cell.cellContent == shape)
				{
					shapeCount++;
					cells.Add(cell);
				}
				else
				{
					shapeCount = 0;
					cells.Clear();
				}
				//shapeCount = cell.cellContent == shape ? shapeCount + 1 : 0;
				if (shapeCount == elementsInRowToWin) return (true, cells);
			}
			shapeCount = 0;
			cells.Clear();
		}

		return (false, null);
	}

	private (bool, List<TicTacToeCell>?) TryFindWinnerVertically(CellContent shape)
	{
		int shapeCount = 0;
		List<TicTacToeCell> cells = new();
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				TicTacToeCell cell = table[i, j];
				if (cell.cellContent == shape)
				{
					shapeCount++;
					cells.Add(cell);
				}
				else
				{
					shapeCount = 0;
					cells.Clear();
				}
				if (shapeCount == elementsInRowToWin) return (true, cells);
			}
			shapeCount = 0;
			cells.Clear();
		}

		return (false, null);
	}

	private (bool, List<TicTacToeCell>?) TryFindWinnerFromTopLeft(CellContent shape)
	{
		List<TicTacToeCell> cells = new();
		// Check diagonals from top-left to bottom-right
		for (int startCol = 0; startCol <= tableSize - elementsInRowToWin; startCol++)
		{
			for (int startRow = 0; startRow <= tableSize - elementsInRowToWin; startRow++)
			{
				int shapeCount = 0;
				cells.Clear();
				for (int i = 0; i < elementsInRowToWin; i++)
				{
					TicTacToeCell cell = table[startRow + i, startCol + i];
					if (cell.cellContent == shape)
					{
						shapeCount++;
						cells.Add(cell);
					}
					else
					{
						shapeCount = 0;
						cells.Clear();
					}
					if (shapeCount == elementsInRowToWin) return (true, cells);
				}
			}
		}

		cells.Clear();
		// Check diagonals from top-right to bottom-left
		for (int startCol = elementsInRowToWin - 1; startCol < tableSize; startCol++)
		{
			for (int startRow = 0; startRow <= tableSize - elementsInRowToWin; startRow++)
			{
				int shapeCount = 0;
				cells.Clear();
				for (int i = 0; i < elementsInRowToWin; i++)
				{
					TicTacToeCell cell = table[startRow + i, startCol - i];
					if (cell.cellContent == shape)
					{
						shapeCount++;
						cells.Add(cell);
					}
					else
					{
						shapeCount = 0;
						cells.Clear();
					}
					if (shapeCount == elementsInRowToWin) return (true, cells);
				}
			}
		}

		return (false, null);
	}
}