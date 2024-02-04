using Common.CommonTools.Log;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using WpfNet6.ViewModel;

namespace WpfNet6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Dispatcher? UIDispatcher { get; private set; }

        public App()
        {
            UIDispatcher = Application.Current.Dispatcher;
            this.DispatcherUnhandledException += CatchUIUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CatchUnhandledException;
        }

        private void CatchUIUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.LogError($"UI线程未处理异常：{e.Exception.Message}\n{e.Exception.StackTrace}");
        }

        private void CatchUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
                LogHelper.LogError($"非UI线程未处理异常：{ex.Message}\n{ex.StackTrace}");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            Mutex mutex = new Mutex(true, p.ProcessName, out bool createNew);
            if (!createNew)
            {
                MessageBox.Show("Application is already run!");
                this.Shutdown();
            }
        }
    }
}