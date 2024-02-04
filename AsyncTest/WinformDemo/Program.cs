using System;
using System.Threading;
using System.Windows.Forms;

namespace WinformDemo
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var mutex = new Mutex(false, "TempDemoMutex", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show("已有正在运行的进程！");
                return;
                //Environment.Exit(1);
            }

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += UIThreadException;
            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //LogHelper.LogError($"UI线程未处理异常:{e.Exception.Message}\n{e.Exception.StackTrace}");
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            //if (e.ExceptionObject is Exception error)
            //LogHelper.LogError($"非UI线程未处理异常:{error.Message}\n{error.StackTrace}");
        }
    }
}