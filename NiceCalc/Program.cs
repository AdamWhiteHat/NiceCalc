using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NiceCalc
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.SetHighDpiMode(HighDpiMode.SystemAware);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.Run(new MainForm());
		}

		public const string LogFilename = "Exception.log.txt";

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Exception ex = (Exception)e.ExceptionObject;

				List<string> lines = new List<string>();
				lines.Add($"[{DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}] : Unhandled Exception Caught");

				if (ex != null)
				{
					lines.Add(ex.ToString());
				}
				else
				{
					lines.Add("(null)");
				}
				lines.Add(string.Empty);

				File.AppendAllLines(LogFilename, lines);
			}
			catch
			{
			}
		}
	}
}