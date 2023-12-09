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

	public AdvertisementController(string adFolderPath, PictureBox billboard)
	{
		IEnumerable<string> files = Directory.EnumerateFiles(adFolderPath);
		int fileCount = files.Count();
		if (fileCount == 0) throw new ArgumentException("Folder is empty");

		adsFileNames = files.ToArray();
		ads = new Bitmap[fileCount];
		this.billboard = billboard;
		billboard.Image = new Bitmap(billboard.Width, billboard.Height);
		timer = new Timer() { Interval = 5000 };
		timer.Tick += Timer_Tick;
		Timer_Tick(null, null);
		billboard.Click += Billboard_Click;

		FillAdsArray();
	}

	private void FillAdsArray()
	{
		for (int i = 0; i < adsFileNames.Length; i++)
		{
			ads[i] = new Bitmap(adsFileNames[i]);
		}
	}

	private void Timer_Tick(object? sender, EventArgs? e)
	{
		int index = random.Next(0, adsFileNames.Length);
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
