using System.Linq;
using System.Data.Entity;

namespace LEARNING_EF_CODE_FIRST
{
	static class Program
	{
		static Program()
		{
		}

		[System.STAThread]
		static void Main()
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

			MainForm frmMain = new MainForm();
			System.Windows.Forms.Application.Run(frmMain);
			if (frmMain != null)
			{
				if (frmMain.IsDisposed == false)
				{
					frmMain.Dispose();
				}
				frmMain = null;
			}
		}
	}
}
