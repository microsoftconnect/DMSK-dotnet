//
//  Report.Designer.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

namespace S_SpeechAnywhere
{
	partial class Report
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbMedicalHistory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFindings = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.buttonCustomizeSpeechBar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Medical history:";
            // 
            // tbMedicalHistory
            // 
            this.tbMedicalHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMedicalHistory.Location = new System.Drawing.Point(34, 78);
            this.tbMedicalHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMedicalHistory.Multiline = true;
            this.tbMedicalHistory.Name = "tbMedicalHistory";
            this.tbMedicalHistory.Size = new System.Drawing.Size(702, 112);
            this.tbMedicalHistory.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 220);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Findings:";
            // 
            // tbFindings
            // 
            this.tbFindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFindings.Location = new System.Drawing.Point(34, 245);
            this.tbFindings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFindings.Multiline = true;
            this.tbFindings.Name = "tbFindings";
            this.tbFindings.Size = new System.Drawing.Size(702, 112);
            this.tbFindings.TabIndex = 4;
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecord.Location = new System.Drawing.Point(626, 18);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(112, 35);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(626, 392);
            this.btClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(112, 35);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // buttonCustomizeSpeechBar
            // 
            this.buttonCustomizeSpeechBar.Location = new System.Drawing.Point(409, 18);
            this.buttonCustomizeSpeechBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCustomizeSpeechBar.Name = "buttonCustomizeSpeechBar";
            this.buttonCustomizeSpeechBar.Size = new System.Drawing.Size(188, 35);
            this.buttonCustomizeSpeechBar.TabIndex = 6;
            this.buttonCustomizeSpeechBar.Text = "Customize speech bar";
            this.buttonCustomizeSpeechBar.UseVisualStyleBackColor = true;
            this.buttonCustomizeSpeechBar.Click += new System.EventHandler(this.buttonCustomizeSpeechBar_Click);
            // 
            // Report
            // 
            this.AcceptButton = this.btnRecord;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(772, 446);
            this.Controls.Add(this.buttonCustomizeSpeechBar);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.tbFindings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMedicalHistory);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(786, 474);
            this.Name = "Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Report_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbMedicalHistory;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbFindings;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button buttonCustomizeSpeechBar;
    }
}

