using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe;

internal static class Extensions
{
	public static void SaveWithDate(this Bitmap bitmap)
	{
		bitmap.Save($"../../../testing/{DateTime.Now.Ticks}.png");
	}
}
