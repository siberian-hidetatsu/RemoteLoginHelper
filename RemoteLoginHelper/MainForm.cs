using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace RemoteLoginHelper
{
	/// <summary>
	/// MainForm Class
	/// </summary>
	public partial class MainForm : Form
	{
		// Win32 API の宣言

		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// ウィンドウのテキストを取得するための関数
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// ウィンドウのクラス名を取得するための関数
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// SendMessage関数 - テキストボックスとのやり取りに使用
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		// ウィンドウメッセージの定数
		const uint WM_GETTEXT = 0x000D;
		const uint WM_GETTEXTLENGTH = 0x000E;
		const uint WM_SETTEXT = 0x000C;

		// EnumChildWindows のコールバック用デリゲート
		delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		private readonly NotifyIcon trayIcon;

		public MainForm()
		{
			InitializeComponent();

			// 初期値設定
			labelWindowTitle.Text = ConfigurationManager.AppSettings["windowTitle"];
			textBoxUserID.Text = ConfigurationManager.AppSettings["userid"];
			textBoxPassword.Text = ConfigurationManager.AppSettings["password"];
			textBoxInterval.Text = ConfigurationManager.AppSettings["interval"];

			// タイマー設定
			timer1.Interval = int.Parse(textBoxInterval.Text);
			timer1.Tick += Timer1_Tick;
			timer1.Start();

			// タスクトレイアイコン設定
			trayIcon = new NotifyIcon
			{
				Icon = this.Icon,
				Text = "Remote Login Helper",
				Visible = false
			};

			trayIcon.DoubleClick += (s, e) =>
			{
				this.Show();
				this.WindowState = FormWindowState.Normal;
				trayIcon.Visible = false;
			};

			// 右クリックメニューの作成
			var contextMenu = new ContextMenuStrip
			{
				ShowImageMargin = false
			};
			var exitItem = new ToolStripMenuItem("Exit", null, OnExitClicked);
			contextMenu.Items.Add(exitItem);
			trayIcon.ContextMenuStrip = contextMenu;

			void OnExitClicked(object sender, EventArgs e)
			{
				trayIcon.Visible = false;
				trayIcon.Dispose();
				Environment.Exit(0);
			}

			// 最小化イベントハンドラ登録
			this.Resize += MainForm_Resize;
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			// "リモートログイン" ウィンドウがあるか確認
			IntPtr parentHandle = FindWindow(null, labelWindowTitle.Text);
			if (parentHandle != IntPtr.Zero )
			{
				SetForegroundWindow(parentHandle); // ウィンドウを前面に

				// 少し待つ（必要なら）
				//System.Threading.Thread.Sleep(200);

				SendKeys.SendWait(" "/*textBoxUserID.Text*/);	// プレースホルダーを消すためのダミー
				System.Threading.Thread.Sleep(200);

				SendKeys.SendWait("{TAB}");						// プレースホルダーを消すため
				System.Threading.Thread.Sleep(200);

				//SendKeys.SendWait(textBoxPassword.Text);
				//System.Threading.Thread.Sleep(200);

#if true
				Console.WriteLine($"親ウィンドウのハンドル: {parentHandle}");
				//Console.WriteLine("\n方法1: FindWindowEx を使用して子ウィンドウを列挙");
				//EnumerateChildWindowsUsingFindWindowEx(parentHandle);

				//Console.WriteLine("\n方法2: EnumChildWindows を使用して子ウィンドウを列挙");
				//EnumerateChildWindowsUsingEnumChildWindows(parentHandle);

				//Console.WriteLine("\n方法3: Editクラスのテキストボックスを検索して内容を取得");
				//FindEditTextBoxes(parentHandle);

				Console.WriteLine("\nテキストボックスに対してテキストを設定するデモ");
				SetTextToEditBoxes(parentHandle);

				//Console.WriteLine("\nプログラムを終了するには何かキーを押してください...");
				//Console.ReadKey();
#endif

				System.Threading.Thread.Sleep(200);
				SendKeys.SendWait("{ENTER}");

				textBoxLogMessage.Text = $"[{DateTime.Now}] リモートログインウィンドウに送信しました。\r\n";

				//timer1.Interval = int.Parse(textBoxInterval.Text) * 2;
			}
			else
			{
				//timer1.Interval = int.Parse(textBoxInterval.Text);
			}
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			if ( this.WindowState == FormWindowState.Minimized )
			{
				this.Hide();
				trayIcon.Visible = true;
			}
		}

		// 方法1: FindWindowEx を使用して子ウィンドウを列挙
		static void EnumerateChildWindowsUsingFindWindowEx(IntPtr parentHandle)
		{
			IntPtr childHandle = IntPtr.Zero;
			int childCount = 0;

			// 子ウィンドウを順番に検索
			do
			{
				// 前回見つかった子ウィンドウの次の子ウィンドウを検索
				childHandle = FindWindowEx(parentHandle, childHandle, null, null);

				if (childHandle != IntPtr.Zero)
				{
					childCount++;
					DisplayWindowInfo(childHandle, childCount);
				}
			}
			while (childHandle != IntPtr.Zero);

			if (childCount == 0)
			{
				Console.WriteLine("子ウィンドウは見つかりませんでした。");
			}
		}

		// 方法2: EnumChildWindows を使用して子ウィンドウを列挙
		static void EnumerateChildWindowsUsingEnumChildWindows(IntPtr parentHandle)
		{
			List<IntPtr> childHandles = new List<IntPtr>();

			// EnumChildWindows を使用してすべての子ウィンドウを列挙
			EnumChildWindows(parentHandle, (hwnd, lParam) =>
			{
				childHandles.Add(hwnd);
				return true; // trueを返して列挙を続行
			}, IntPtr.Zero);

			if (childHandles.Count == 0)
			{
				Console.WriteLine("子ウィンドウは見つかりませんでした。");
			}
			else
			{
				for (int i = 0; i < childHandles.Count; i++)
				{
					DisplayWindowInfo(childHandles[i], i + 1);
				}
			}
		}

		// ウィンドウの情報を表示する関数
		static void DisplayWindowInfo(IntPtr hWnd, int index)
		{
			// ウィンドウのタイトルを取得
			StringBuilder windowText = new StringBuilder(256);
			GetWindowText(hWnd, windowText, windowText.Capacity);

			// ウィンドウのクラス名を取得
			StringBuilder className = new StringBuilder(256);
			GetClassName(hWnd, className, className.Capacity);

			Console.WriteLine($"子ウィンドウ #{index}:");
			Console.WriteLine($"  ハンドル: {hWnd}");
			Console.WriteLine($"  クラス名: {className}");
			Console.WriteLine($"  ウィンドウテキスト: {windowText}");

			// Editクラスのテキストボックスの場合、内容を取得して表示
			if (className.ToString() == "Edit")
			{
				string editText = GetEditText(hWnd);
				Console.WriteLine($"  テキストボックスの内容: {editText}");
			}
		}

		// "Edit"クラスのテキストボックスを検索してその内容を取得する関数
		static void FindEditTextBoxes(IntPtr parentHandle)
		{
			List<IntPtr> editHandles = new List<IntPtr>();

			// すべての子ウィンドウを列挙
			EnumChildWindows(parentHandle, (hwnd, lParam) =>
			{
				StringBuilder className = new StringBuilder(256);
				GetClassName(hwnd, className, className.Capacity);

				// "Edit"クラスのウィンドウを見つけたらリストに追加
				if (className.ToString() == "Edit")
				{
					editHandles.Add(hwnd);
				}
				return true; // 列挙を続行
			}, IntPtr.Zero);

			if (editHandles.Count == 0)
			{
				Console.WriteLine("Editクラスのテキストボックスは見つかりませんでした。");
			}
			else
			{
				for (int i = 0; i < editHandles.Count; i++)
				{
					IntPtr editHandle = editHandles[i];
					string text = GetEditText(editHandle);

					Console.WriteLine($"テキストボックス #{i + 1}:");
					Console.WriteLine($"  ハンドル: {editHandle}");
					Console.WriteLine($"  内容: {text}");
				}
			}
		}

		// テキストボックスの内容を取得する関数
		static string GetEditText(IntPtr hWnd)
		{
			// まずテキストの長さを取得
			IntPtr length = SendMessage(hWnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
			int textLength = length.ToInt32();

			if (textLength > 0)
			{
				// テキストを取得するためのバッファを用意
				StringBuilder sb = new StringBuilder(textLength + 1);
				SendMessage(hWnd, WM_GETTEXT, new IntPtr(sb.Capacity), sb);
				return sb.ToString();
			}

			return string.Empty;
		}

		// テキストボックスへテキストを設定するデモ関数
		void SetTextToEditBoxes(IntPtr parentHandle)
		{
			List<IntPtr> editHandles = new List<IntPtr>();

			// すべての子ウィンドウを列挙して"Edit"クラスのウィンドウを検索
			EnumChildWindows(parentHandle, (hwnd, lParam) =>
			{
				StringBuilder className = new StringBuilder(256);
				GetClassName(hwnd, className, className.Capacity);

				if (className.ToString() == "Edit")
				{
					editHandles.Add(hwnd);
				}
				return true; // 列挙を続行
			}, IntPtr.Zero);

			if (editHandles.Count == 0)
			{
				Console.WriteLine("Editクラスのテキストボックスは見つかりませんでした。");
				return;
			}

			// 見つかった各テキストボックスに新しいテキストを設定
			for (int i = 0; i < editHandles.Count; i++)
			{
				IntPtr editHandle = editHandles[i];

				// 現在のテキストを取得して表示
				string currentText = GetEditText(editHandle);
				Console.WriteLine($"テキストボックス #{i + 1} (ハンドル: {editHandle})");
				Console.WriteLine($"  現在の内容: {currentText}");

				// 新しいテキストを設定 (英数字と日本語両方を含む)
				string newText = $"TextBox#{i + 1}: This is a new text! これは新しいテキスト{i + 1}です！123";

				if (i == 7)
				{
					newText = textBoxUserID.Text;
				}
				else if (i == 10)
				{
					newText = textBoxPassword.Text;
				}
				else
				{
					continue;
				}

				Console.WriteLine($"  設定しようとしているテキスト: {newText}");
				bool success = SetTextToEditBox(editHandle, newText);

				if (success)
				{
					Console.WriteLine($"  テキスト設定成功");

					// 設定後のテキストを確認
					string updatedText = GetEditText(editHandle);
					Console.WriteLine($"  確認 - 設定後の内容: {updatedText}");
				}
				else
				{
					Console.WriteLine("  テキストの設定に失敗しました。");
				}

				Console.WriteLine(); // テキストボックス間に空行を挿入
			}
		}

		// テキストボックスに新しいテキストを設定する関数
		static bool SetTextToEditBox(IntPtr hWnd, string text)
		{
			// WM_SETTEXT メッセージを送信してテキストを設定
			IntPtr result = SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, text);

			// SendMessageが成功すると、通常は非ゼロの値が返されます
			return result != IntPtr.Zero;
		}
	}
}
