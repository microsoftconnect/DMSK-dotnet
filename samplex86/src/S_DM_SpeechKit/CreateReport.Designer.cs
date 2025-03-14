//
//  CreateReport.Designer.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

namespace S_SpeechAnywhere
{
	partial class CreateReport
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
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCustomizeSpeechBar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create a report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCustomizeSpeechBar
            // 
            this.buttonCustomizeSpeechBar.Location = new System.Drawing.Point(54, 106);
            this.buttonCustomizeSpeechBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCustomizeSpeechBar.Name = "buttonCustomizeSpeechBar";
            this.buttonCustomizeSpeechBar.Size = new System.Drawing.Size(188, 37);
            this.buttonCustomizeSpeechBar.TabIndex = 4;
            this.buttonCustomizeSpeechBar.Text = "Customize speech bar";
            this.buttonCustomizeSpeechBar.UseVisualStyleBackColor = true;
            this.buttonCustomizeSpeechBar.Click += new System.EventHandler(this.buttonCustomizeSpeechBar_Click);
            // 
            // CreateReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 158);
            this.Controls.Add(this.buttonCustomizeSpeechBar);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CreateReport";
            this.Text = "Create report";
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCustomizeSpeechBar;
    }
}