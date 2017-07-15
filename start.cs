using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace start
{
	class Program
	{
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;
		const int SW_SHOW = 5;

		static List<string> GetProcess()
		{
			List<string> pr = new List<string>();
			Process[] processes = Process.GetProcesses();
			foreach (Process p in processes)
				if (!String.IsNullOrEmpty(p.MainWindowTitle))
					pr.Add(p.MainWindowTitle);
			return pr;
		}

		static void Main()
		{
			Console.Title = "launcher17";
			var handle = GetConsoleWindow();
			ShowWindow(handle, SW_HIDE);
			bool is_tudo_bom;
			while (true)
			{
				is_tudo_bom = false;
				foreach (string p in GetProcess())
					if (p.Equals("2 0 1 7"))
					{
						is_tudo_bom = true;
						break;
					}
				if (File.Exists("stop") || !DateTime.Today.Year.ToString().Equals("2017"))
					break;
				if(!is_tudo_bom)
					try { Process.Start("2017.exe"); } catch { break; }
				Thread.Sleep(500);
			}
		}
	}
}