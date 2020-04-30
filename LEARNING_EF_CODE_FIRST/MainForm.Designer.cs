namespace LEARNING_EF_CODE_FIRST
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

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
			this.txtCountryName = new System.Windows.Forms.TextBox();
			this.txtCountryCodeFrom = new System.Windows.Forms.TextBox();
			this.txtCountryCodeTo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtCountryName
			// 
			this.txtCountryName.Location = new System.Drawing.Point(12, 12);
			this.txtCountryName.Name = "txtCountryName";
			this.txtCountryName.Size = new System.Drawing.Size(266, 20);
			this.txtCountryName.TabIndex = 0;
			// 
			// txtCountryCodeFrom
			// 
			this.txtCountryCodeFrom.Location = new System.Drawing.Point(12, 38);
			this.txtCountryCodeFrom.Name = "txtCountryCodeFrom";
			this.txtCountryCodeFrom.Size = new System.Drawing.Size(266, 20);
			this.txtCountryCodeFrom.TabIndex = 1;
			// 
			// txtCountryCodeTo
			// 
			this.txtCountryCodeTo.Location = new System.Drawing.Point(12, 64);
			this.txtCountryCodeTo.Name = "txtCountryCodeTo";
			this.txtCountryCodeTo.Size = new System.Drawing.Size(266, 20);
			this.txtCountryCodeTo.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 262);
			this.Controls.Add(this.txtCountryCodeTo);
			this.Controls.Add(this.txtCountryCodeFrom);
			this.Controls.Add(this.txtCountryName);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Main";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtCountryName;
		private System.Windows.Forms.TextBox txtCountryCodeFrom;
		private System.Windows.Forms.TextBox txtCountryCodeTo;




	}
}
