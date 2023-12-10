namespace TicTacToe
{
	partial class ResizeMenu
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			textBoxScaleX = new TextBox();
			label1 = new Label();
			groupBox1 = new GroupBox();
			buttonOK = new Button();
			groupBox3 = new GroupBox();
			textBoxScaleY = new TextBox();
			groupBox2 = new GroupBox();
			groupBox1.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			// 
			// textBoxScaleX
			// 
			textBoxScaleX.Location = new Point(21, 32);
			textBoxScaleX.Name = "textBoxScaleX";
			textBoxScaleX.Size = new Size(97, 23);
			textBoxScaleX.TabIndex = 0;
			textBoxScaleX.Text = "-0.05";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(6, 19);
			label1.Name = "label1";
			label1.Size = new Size(366, 15);
			label1.TabIndex = 2;
			label1.Text = "Zvolte posunutí v měřítku škálování (např. -0.05 => zmenšení o 5 %)";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(buttonOK);
			groupBox1.Controls.Add(groupBox3);
			groupBox1.Controls.Add(groupBox2);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(386, 193);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Změna velikosti";
			// 
			// buttonOK
			// 
			buttonOK.Location = new Point(6, 124);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new Size(366, 56);
			buttonOK.TabIndex = 5;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(textBoxScaleY);
			groupBox3.Location = new Point(202, 37);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(149, 81);
			groupBox3.TabIndex = 4;
			groupBox3.TabStop = false;
			groupBox3.Text = "Osa Y";
			// 
			// textBoxScaleY
			// 
			textBoxScaleY.Location = new Point(21, 32);
			textBoxScaleY.Name = "textBoxScaleY";
			textBoxScaleY.Size = new Size(97, 23);
			textBoxScaleY.TabIndex = 0;
			textBoxScaleY.Text = "-0.05";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(textBoxScaleX);
			groupBox2.Location = new Point(36, 37);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(149, 81);
			groupBox2.TabIndex = 3;
			groupBox2.TabStop = false;
			groupBox2.Text = "Osa X";
			// 
			// ResizeMenu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(407, 215);
			Controls.Add(groupBox1);
			Name = "ResizeMenu";
			Text = "Konfigurace";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TextBox textBoxScaleX;
		private Label label1;
		private GroupBox groupBox1;
		private GroupBox groupBox3;
		private TextBox textBoxScaleY;
		private GroupBox groupBox2;
		private Button buttonOK;
	}
}