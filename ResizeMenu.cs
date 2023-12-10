using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
	public partial class ResizeMenu : Form
	{
		const float minScale = -0.5f;
		const float maxScale = 0.5f;
		public float offsetScaleX = -0.05f;
		public float offsetScaleY = -0.05f;
		private bool xScaleOK = true;
		private bool yScaleOK = true;
		public ResizeMenu()
		{
			InitializeComponent();
			Icon = new Icon("../../../resources/settings.ico");
			textBoxScaleX.TextChanged += TextBoxScaleX_TextChanged;
			textBoxScaleY.TextChanged += TextBoxScaleY_TextChanged;
			FormClosing += ResizeMenu_FormClosing;
			buttonOK.Click += (s, e) => Close();
		}

		private void ResizeMenu_FormClosing(object? sender, FormClosingEventArgs e)
		{
			e.Cancel = !xScaleOK || !yScaleOK;
		}

		private void TextBoxScaleY_TextChanged(object? sender, EventArgs e)
		{
			if (float.TryParse(textBoxScaleY.Text, out float scale) && IsWithinBounds(scale))
			{
				yScaleOK = true;
				offsetScaleY = scale;
			}
			else if (textBoxScaleY.Text == string.Empty)
			{
				yScaleOK = true;
				offsetScaleY = 0f;
			}
			else yScaleOK = false;

			buttonOK.Enabled = xScaleOK && yScaleOK;
		}

		private void TextBoxScaleX_TextChanged(object? sender, EventArgs e)
		{
			if (float.TryParse(textBoxScaleX.Text, out float scale) && IsWithinBounds(scale))
			{
				xScaleOK = true;
				offsetScaleX = scale;
			}
			else if (textBoxScaleX.Text == string.Empty)
			{
				xScaleOK = true;
				offsetScaleX = 0f;
			}
			else xScaleOK = false;

			buttonOK.Enabled = xScaleOK && yScaleOK;
		}

		private bool IsWithinBounds(float num)
		{
			return minScale <= num && num <= maxScale;
		}
	}
}
