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
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new MainForm());
        }

        public const string ExceptionLogPath = "Exception.log.txt";

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        private static void HandleException(Exception ex)
        {
            try
            {
                List<string> lines = new List<string>()
                {
                    Environment.NewLine
                };

                string exceptionTypeMessage = "";
                if (ex != null)
                {
                    exceptionTypeMessage = $"Unhandled {{ex.GetType().Namespace}}.{{ex.GetType().Name}} Caught!";
                }

                lines.Add($"[{DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}] : {exceptionTypeMessage}");

                if (ex != null)
                {
                    lines.Add(ex.ToString());
                }
                else
                {
                    lines.Add("(null)");
                }

                lines.Add(string.Empty);

                File.AppendAllLines(ExceptionLogPath, lines);
            }
            catch
            {
            }
        }
    }
}