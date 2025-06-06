using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteLoginHelper
{
	internal static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// ミューテックスを使用して、アプリケーションのインスタンスが一つだけであることを保証します。
			using (var mutex = new System.Threading.Mutex(false, "RemoteLoginHelperMutex"))
			{
				if (!mutex.WaitOne(0, false))
				{
					MessageBox.Show("すでに実行中です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// アプリケーションの初期化
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
	}
}
