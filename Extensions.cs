namespace TicTacToe;

internal static class Extensions
{
	public static void SaveWithDate(this Bitmap bitmap)
	{
		bitmap.Save($"../../../testing/{DateTime.Now.Ticks}.png");
	}
}
