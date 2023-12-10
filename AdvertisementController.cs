using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace TicTacToe;

internal class AdvertisementController
{
	string[] adsFileNames;
	Bitmap[] ads;
	PictureBox billboard;
	private uint _refreshDelay;
	public uint RefreshDelay { get => _refreshDelay;
		set
		{
			_refreshDelay = value;
			timer.Interval = (int)value;
		}
	}
	private Timer timer;
	private Random random = new Random();
	private int index;

	public AdvertisementController(string adFolderPath, PictureBox billboard)
	{
		IEnumerable<string> files = Directory.EnumerateFiles(adFolderPath);
		int fileCount = files.Count();
		if (fileCount == 0) throw new ArgumentException("Folder is empty");

		adsFileNames = files.ToArray();
		ads = new Bitmap[fileCount];
		this.billboard = billboard;
		Bitmap b = new Bitmap(Convert.ToInt32(billboard.Width / TicTacToe.scaleX), Convert.ToInt32(billboard.Height / TicTacToe.scaleY));
		Graphics.FromImage(b).Clear(Color.White);
		billboard.Image = b;
		timer = new Timer() { Interval = 5000 };
		timer.Tick += Timer_Tick;
		billboard.Click += Billboard_Click;

		FillAdsArray();
	}

	private void FillAdsArray()
	{
		for (int i = 0; i < adsFileNames.Length; i++)
		{
			Bitmap originalSize = new Bitmap(adsFileNames[i]);
			ads[i] = new Bitmap(originalSize, billboard.Width, billboard.Height);
		}
	}

	private void Timer_Tick(object? sender, EventArgs? e)
	{
		index = random.Next(0, adsFileNames.Length);
		billboard.Image = ads[index];
	}

	private void Billboard_Click(object? sender, EventArgs e)
	{
		GiveAuthorCredit();
	}

	public static void GiveAuthorCredit()
	{
		Process.Start(new ProcessStartInfo() { UseShellExecute = true, FileName = "https://github.com/IgnacBrychta" });
	}

	public void Start()
	{
		timer.Enabled = true;
	}

	public void Stop()
	{
		timer.Enabled = false;
	}
}
