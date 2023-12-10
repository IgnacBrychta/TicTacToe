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
	bool erasing = false;
	readonly Cursor eraser = new Cursor("../../../resources/rubber eraser.ico");
	public static float scaleX;
	public static float scaleY;
	public static float offsetScaleX = 0f;
	public static float offsetScaleY = 0f;
	public RectangleF resizedScreen;
	Rectangle originalScreen = new Rectangle(0, 0, 2560, 1600);
	public TicTacToe()
	{
		if (Screen.PrimaryScreen is null) throw new Exception("No screen connected");

		if(Screen.PrimaryScreen.WorkingArea.Width != originalScreen.Width || Screen.PrimaryScreen.WorkingArea.Height != originalScreen.Height)
		{
			ResizeMenu resizeMenu = new ResizeMenu();
			_ = resizeMenu.ShowDialog();
			offsetScaleX = -resizeMenu.offsetScaleX; 
			offsetScaleY = -resizeMenu.offsetScaleY;
		}
		InitializeComponent();
		SetControlTheme(this);
		SetColorTheme(this);
		CalculateScaleFactor();
		ResizeAllControls(this);
		CreateTableArray(table);
		WindowState = FormWindowState.Maximized;
		ConfigureUI();
		Bitmap bitmap = new Bitmap(tableScreen.Width, tableScreen.Height);// (Bitmap)tableScreen.Image
		TicTacToeDrawer.DrawTable(bitmap, table);
		tableScreen.Image = bitmap;
		SetSubscriptions();
		Icon = new Icon("../../../resources/icon.ico");
		new AdvertisementController("../../../resources/ads", sponsoredPictureBox) { RefreshDelay = 6000 }.Start();
		TicTacToeDrawer.cellWidth = Convert.ToInt32(TicTacToeDrawer.cellWidth / scaleX);
#if !IgnoreDisplayWarning
		MessageBox.Show(
			"Tento program byl vyvíjen pro 10\" 2560 px × 1600 px displej GPD Win Max 2, je možné, že se na tomto displeji nebude vše zobrazovat správně.",
			"Upozornìní",
			MessageBoxButtons.OK,
			MessageBoxIcon.Warning
			);
#endif
	}

	private void ResizeAllControls(Control controls)
	{
		foreach (Control control in controls.Controls)
		{
			ResizeAllControls(control);
			control.Size = new Size(Convert.ToInt32(control.Size.Width * resizedScreen.Width), Convert.ToInt32(control.Size.Height * resizedScreen.Height));
			control.Location = new Point(Convert.ToInt32(control.Location.X * resizedScreen.Width), Convert.ToInt32(control.Location.Y * resizedScreen.Width));
		}
	}

	private void CalculateScaleFactor()
	{
		Rectangle currentScreen = Screen.PrimaryScreen!.WorkingArea;

		scaleX = originalScreen.Width * 1f / currentScreen.Width + offsetScaleX;
		scaleY = originalScreen.Height * 1f / currentScreen.Height + offsetScaleY;

		resizedScreen = new RectangleF(0f, 0f, scaleX, scaleY);

	} 

	private void SetSubscriptions()
	{
		tableScreen.MouseDown += TicTacToe_MouseDown;
		buttonRestart.Click += ButtonRestart_Click;
		buttonErase.Click += ButtonErase_Click;
		tableScreen.MouseMove += TableScreen_MouseMove;
		Load += TicTacToe_Load;
	}

	private void TicTacToe_Load(object? sender, EventArgs e)
	{
		MaximumSize = Size;
		MinimumSize = Size;
	}

	private void TableScreen_MouseMove(object? sender, MouseEventArgs e)
	{
		TicTacToeCell? cell = FindHitCell(e.X, e.Y);
		if (cell is null) return;

		Bitmap bitmap = (Bitmap)tableScreen.Image;
		Graphics g = Graphics.FromImage(bitmap);
		TicTacToeDrawer.ClearScreen(bitmap, Color.Black);
		TicTacToeDrawer.DrawTable(bitmap, table);
		if (currentPlayerWon) TicTacToeDrawer.DrawHighlightedCells(g, table);
		TicTacToeDrawer.HighlightSingleCell(cell, g);
		tableScreen.Image = bitmap;
	}

	private void StopErasing()
	{
		erasing = false;
		Cursor = Cursors.Hand;
	}

	private void ButtonErase_Click(object? sender, EventArgs e)
	{
		if (currentPlayerWon) return;

		if (erasing)
		{
			SwitchCurrentPlayer();
			StopErasing();
			return;
		}

		if (!IsTableEmpty()) return;

		erasing = true;
		SwitchCurrentPlayer();
		Cursor = eraser;
	}

	private bool IsTableEmpty()
	{
		for (int i = 0; i < tableSize; i++)
		{
			for (int j = 0; j < tableSize; j++)
			{
				if (table[i, j].cellContent != CellContent.None) return true;
			}
		}

		return false;
	}

	private void ButtonRestart_Click(object? sender, EventArgs e)
	{
		currentPlayerWon = false;
		StopErasing();
		SwitchCurrentPlayer();
		CreateTableArray(table);
		DrawTable();
	}

	private void TicTacToe_MouseDown(object? sender, MouseEventArgs e)
	{
		TicTacToeCell? cell = FindHitCell(e.X, e.Y);
		if (cell is null || currentPlayerWon) return;

		if (erasing)
		{
			if (cell.cellContent != currentPlayer) return;
			cell.cellContent = CellContent.None;
			StopErasing();
			DrawTable();
			return;
		}


		HandleCellSelect(cell);
	}

	private void HandleCellSelect(TicTacToeCell cell)
	{
		if (cell.cellContent != CellContent.None) return;

		cell.cellContent = currentPlayer;

		(bool horizontalWin, List<TicTacToeCell>? cellsHorizontal) =					TryFindWinnerHorizontally(currentPlayer);
		(bool verticalWin, List<TicTacToeCell>? cellsVertical) =						TryFindWinnerVertically(currentPlayer);
		(bool diagonalTopLeftWin, List<TicTacToeCell>? cellsTopLeftDiagonal) =			TryFindWinnerFromTopLeft(currentPlayer);
		(bool diagonalTopRightWin, List<TicTacToeCell>? cellsTopRightDiagonal) =		TryFindWinnerFromTopRight(currentPlayer);
		(bool diagonalBottomRightWin, List<TicTacToeCell>? cellsBottomRightDiagonal) =	TryFindWinnerFromBottomRight(currentPlayer);
		(bool diagonalBottomLeftWin, List<TicTacToeCell>? cellsBottomLeftDiagonal) =	TryFindWinnerFromBottomLeft(currentPlayer);
		currentPlayerWon = horizontalWin || verticalWin || diagonalTopLeftWin || diagonalTopRightWin || diagonalBottomRightWin || diagonalBottomLeftWin;

		if (currentPlayerWon)
		{
			if (currentPlayer == CellContent.Cross) scoreCrosses++;
			else scoreCircles++;

			// must be AddRange() and not winningCells = cellsHorizontal
			// the player might win in more than one direction
			List<TicTacToeCell> winningCells = new();
			if (horizontalWin)			winningCells.AddRange(cellsHorizontal!);
			if (verticalWin)			winningCells.AddRange(cellsVertical!);
			if (diagonalTopLeftWin)		winningCells.AddRange(cellsTopLeftDiagonal!);
			if (diagonalTopRightWin)	winningCells.AddRange(cellsTopRightDiagonal!);
			if (diagonalBottomRightWin) winningCells.AddRange(cellsBottomRightDiagonal!);
			if (diagonalBottomLeftWin)	winningCells.AddRange(cellsBottomLeftDiagonal!);

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

	private (bool, List<TicTacToeCell>?) TryFindWinnerHorizontally(CellContent shape)
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
				if (shapeCount == elementsInRowToWin)
				{
					var (moreCellsWinningExist, moreCells) = TryFindMoreWinningCellsHorizontally(cell, shape);
					if (moreCellsWinningExist) cells.AddRange(moreCells!.GetRange(0, elementsInRowToWin - 1));
					return (true, cells);
				}
			}
			shapeCount = 0;
			cells.Clear();
		}

		return (false, null);
	}

	private (bool, List<TicTacToeCell>?) TryFindMoreWinningCellsHorizontally(TicTacToeCell ticTacToeCell, CellContent shape)
	{
		int row = ticTacToeCell.anchor.Y;
		List<TicTacToeCell> cells = new();
		int shapeCount = 0;
		for (int i = tableSize - 1; i >= 0; i--)
		{
			TicTacToeCell cell = table[i, row];
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
				if (shapeCount == elementsInRowToWin)
				{
					var (moreCellsWinningExist, moreCells) = TryFindMoreWinningCellsVertically(cell, shape);
					if (moreCellsWinningExist) cells.AddRange(moreCells!.GetRange(0, elementsInRowToWin - 1));
					return (true, cells);
				}
			}
			shapeCount = 0;
			cells.Clear();
		}

		return (false, null);
	}

	private (bool, List<TicTacToeCell>?) TryFindMoreWinningCellsVertically(TicTacToeCell ticTacToeCell, CellContent shape)
	{
		int column = ticTacToeCell.anchor.X;
		List<TicTacToeCell> cells = new();
		int shapeCount = 0;
		for (int i = tableSize - 1; i >= 0; i--)
		{
			TicTacToeCell cell = table[column, i];
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

		return (false, null);
	}

	private (bool, List<TicTacToeCell>?) TryFindWinnerFromBottomRight(CellContent shape)
	{
		List<TicTacToeCell> cells = new();
		// Check diagonals from bottom-right to top-left
		for (int startCol = tableSize - elementsInRowToWin; startCol >= 0; startCol--)
		{
			for (int startRow = tableSize - elementsInRowToWin; startRow >= 0; startRow--)
			{
				int shapeCount = 0;
				cells.Clear();
				for (int i = 0; i < elementsInRowToWin; i++)
				{
					TicTacToeCell cell = table[startRow + i, startCol + i];
					if (cell.anchor.X == 19 && cell.anchor.Y == 19)
					{
						;
					}
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

	private (bool, List<TicTacToeCell>?) TryFindWinnerFromTopRight(CellContent shape)
	{
		List<TicTacToeCell> cells = new();
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

	private (bool, List<TicTacToeCell>?) TryFindWinnerFromBottomLeft(CellContent shape)
	{
		List<TicTacToeCell> cells = new();
		// Check diagonals from bottom-left to top-right
		for (int startCol = tableSize - 1; startCol >= elementsInRowToWin; startCol--)
		{
			for (int startRow = tableSize - elementsInRowToWin - 1; startRow >= 0; startRow--)
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
