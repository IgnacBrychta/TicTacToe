namespace TicTacToe
{
	partial class TicTacToe
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicTacToe));
			groupBox1 = new GroupBox();
			buttonErase = new Button();
			buttonRestart = new Button();
			groupBox3 = new GroupBox();
			scoreCirclesPictureBox = new PictureBox();
			scoreCirclesText = new RichTextBox();
			scoreCrossesPictureBox = new PictureBox();
			scoreCrossesText = new RichTextBox();
			groupBox2 = new GroupBox();
			currentPlayePictureBox = new PictureBox();
			pictureBox1 = new PictureBox();
			groupBox4 = new GroupBox();
			tableScreen = new PictureBox();
			groupBox5 = new GroupBox();
			richTextBox1 = new RichTextBox();
			richTextBoxDisclaimer = new RichTextBox();
			sponsoredPictureBox = new PictureBox();
			groupBox1.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)scoreCirclesPictureBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)scoreCrossesPictureBox).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)currentPlayePictureBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tableScreen).BeginInit();
			groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)sponsoredPictureBox).BeginInit();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(buttonErase);
			groupBox1.Controls.Add(buttonRestart);
			groupBox1.Controls.Add(groupBox3);
			groupBox1.Controls.Add(groupBox2);
			groupBox1.Controls.Add(pictureBox1);
			groupBox1.Location = new Point(6, 6);
			groupBox1.Margin = new Padding(2, 1, 2, 1);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(2, 1, 2, 1);
			groupBox1.Size = new Size(215, 661);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Informace";
			// 
			// buttonErase
			// 
			buttonErase.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			buttonErase.Location = new Point(3, 525);
			buttonErase.Margin = new Padding(2, 1, 2, 1);
			buttonErase.Name = "buttonErase";
			buttonErase.Size = new Size(200, 69);
			buttonErase.TabIndex = 3;
			buttonErase.Text = "Guma";
			buttonErase.UseVisualStyleBackColor = true;
			// 
			// buttonRestart
			// 
			buttonRestart.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			buttonRestart.Location = new Point(3, 433);
			buttonRestart.Margin = new Padding(2, 1, 2, 1);
			buttonRestart.Name = "buttonRestart";
			buttonRestart.Size = new Size(200, 83);
			buttonRestart.TabIndex = 2;
			buttonRestart.Text = "Restart";
			buttonRestart.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(scoreCirclesPictureBox);
			groupBox3.Controls.Add(scoreCirclesText);
			groupBox3.Controls.Add(scoreCrossesPictureBox);
			groupBox3.Controls.Add(scoreCrossesText);
			groupBox3.Location = new Point(3, 211);
			groupBox3.Margin = new Padding(2, 1, 2, 1);
			groupBox3.Name = "groupBox3";
			groupBox3.Padding = new Padding(2, 1, 2, 1);
			groupBox3.Size = new Size(200, 211);
			groupBox3.TabIndex = 1;
			groupBox3.TabStop = false;
			groupBox3.Text = "Skóre";
			// 
			// scoreCirclesPictureBox
			// 
			scoreCirclesPictureBox.Location = new Point(10, 117);
			scoreCirclesPictureBox.Margin = new Padding(2, 1, 2, 1);
			scoreCirclesPictureBox.Name = "scoreCirclesPictureBox";
			scoreCirclesPictureBox.Size = new Size(95, 82);
			scoreCirclesPictureBox.TabIndex = 2;
			scoreCirclesPictureBox.TabStop = false;
			// 
			// scoreCirclesText
			// 
			scoreCirclesText.Font = new Font("Segoe UI", 45F, FontStyle.Regular, GraphicsUnit.Point);
			scoreCirclesText.Location = new Point(121, 117);
			scoreCirclesText.Margin = new Padding(2, 1, 2, 1);
			scoreCirclesText.Name = "scoreCirclesText";
			scoreCirclesText.ReadOnly = true;
			scoreCirclesText.ScrollBars = RichTextBoxScrollBars.None;
			scoreCirclesText.Size = new Size(70, 84);
			scoreCirclesText.TabIndex = 3;
			scoreCirclesText.Text = "0";
			// 
			// scoreCrossesPictureBox
			// 
			scoreCrossesPictureBox.Location = new Point(10, 18);
			scoreCrossesPictureBox.Margin = new Padding(2, 1, 2, 1);
			scoreCrossesPictureBox.Name = "scoreCrossesPictureBox";
			scoreCrossesPictureBox.Size = new Size(95, 82);
			scoreCrossesPictureBox.TabIndex = 1;
			scoreCrossesPictureBox.TabStop = false;
			// 
			// scoreCrossesText
			// 
			scoreCrossesText.Font = new Font("Segoe UI", 45F, FontStyle.Regular, GraphicsUnit.Point);
			scoreCrossesText.Location = new Point(121, 18);
			scoreCrossesText.Margin = new Padding(2, 1, 2, 1);
			scoreCrossesText.Name = "scoreCrossesText";
			scoreCrossesText.ReadOnly = true;
			scoreCrossesText.ScrollBars = RichTextBoxScrollBars.None;
			scoreCrossesText.Size = new Size(70, 84);
			scoreCrossesText.TabIndex = 1;
			scoreCrossesText.Text = "0";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(currentPlayePictureBox);
			groupBox2.Location = new Point(3, 18);
			groupBox2.Margin = new Padding(2, 1, 2, 1);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new Padding(2, 1, 2, 1);
			groupBox2.Size = new Size(200, 186);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Na řadě";
			// 
			// currentPlayePictureBox
			// 
			currentPlayePictureBox.Location = new Point(9, 18);
			currentPlayePictureBox.Margin = new Padding(2, 1, 2, 1);
			currentPlayePictureBox.Name = "currentPlayePictureBox";
			currentPlayePictureBox.Size = new Size(181, 162);
			currentPlayePictureBox.TabIndex = 1;
			currentPlayePictureBox.TabStop = false;
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new Point(491, 18);
			pictureBox1.Margin = new Padding(2, 1, 2, 1);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(106, 62);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// groupBox4
			// 
			groupBox4.Controls.Add(tableScreen);
			groupBox4.Location = new Point(233, 5);
			groupBox4.Margin = new Padding(2, 1, 2, 1);
			groupBox4.Name = "groupBox4";
			groupBox4.Padding = new Padding(2, 1, 2, 1);
			groupBox4.Size = new Size(759, 661);
			groupBox4.TabIndex = 1;
			groupBox4.TabStop = false;
			groupBox4.Text = "Mřížka s piškvorkami";
			// 
			// tableScreen
			// 
			tableScreen.Location = new Point(6, 16);
			tableScreen.Margin = new Padding(2, 1, 2, 1);
			tableScreen.Name = "tableScreen";
			tableScreen.Size = new Size(743, 638);
			tableScreen.TabIndex = 0;
			tableScreen.TabStop = false;
			// 
			// groupBox5
			// 
			groupBox5.Controls.Add(richTextBox1);
			groupBox5.Controls.Add(richTextBoxDisclaimer);
			groupBox5.Controls.Add(sponsoredPictureBox);
			groupBox5.Location = new Point(1002, 6);
			groupBox5.Margin = new Padding(2, 1, 2, 1);
			groupBox5.Name = "groupBox5";
			groupBox5.Padding = new Padding(2, 1, 2, 1);
			groupBox5.Size = new Size(371, 661);
			groupBox5.TabIndex = 2;
			groupBox5.TabStop = false;
			groupBox5.Text = "Sponzorovaný obsah";
			// 
			// richTextBox1
			// 
			richTextBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			richTextBox1.Location = new Point(6, 615);
			richTextBox1.Margin = new Padding(2, 1, 2, 1);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.ReadOnly = true;
			richTextBox1.Size = new Size(363, 33);
			richTextBox1.TabIndex = 2;
			richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// richTextBoxDisclaimer
			// 
			richTextBoxDisclaimer.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
			richTextBoxDisclaimer.Location = new Point(6, 408);
			richTextBoxDisclaimer.Margin = new Padding(2, 1, 2, 1);
			richTextBoxDisclaimer.Name = "richTextBoxDisclaimer";
			richTextBoxDisclaimer.ReadOnly = true;
			richTextBoxDisclaimer.Size = new Size(363, 102);
			richTextBoxDisclaimer.TabIndex = 1;
			richTextBoxDisclaimer.Text = "Upozornění: nejsme schopni ověřit důvěryhodnost inzercí, proto doporučujume být velmi obezřetní.";
			// 
			// sponsoredPictureBox
			// 
			sponsoredPictureBox.Location = new Point(25, 16);
			sponsoredPictureBox.Margin = new Padding(2, 1, 2, 1);
			sponsoredPictureBox.Name = "sponsoredPictureBox";
			sponsoredPictureBox.Size = new Size(332, 394);
			sponsoredPictureBox.TabIndex = 0;
			sponsoredPictureBox.TabStop = false;
			// 
			// TicTacToe
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1417, 664);
			Controls.Add(groupBox5);
			Controls.Add(groupBox4);
			Controls.Add(groupBox1);
			Margin = new Padding(2, 1, 2, 1);
			Name = "TicTacToe";
			Text = "Ignácovy piškvorky";
			groupBox1.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)scoreCirclesPictureBox).EndInit();
			((System.ComponentModel.ISupportInitialize)scoreCrossesPictureBox).EndInit();
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)currentPlayePictureBox).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)tableScreen).EndInit();
			groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)sponsoredPictureBox).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox groupBox1;
		private GroupBox groupBox3;
		private PictureBox scoreCrossesPictureBox;
		private RichTextBox scoreCrossesText;
		private GroupBox groupBox2;
		private PictureBox currentPlayePictureBox;
		private PictureBox pictureBox1;
		private PictureBox scoreCirclesPictureBox;
		private RichTextBox scoreCirclesText;
		private GroupBox groupBox4;
		private PictureBox tableScreen;
		private Button buttonRestart;
		private GroupBox groupBox5;
		private PictureBox sponsoredPictureBox;
		private RichTextBox richTextBoxDisclaimer;
		private Button buttonErase;
		private RichTextBox richTextBox1;
	}
}