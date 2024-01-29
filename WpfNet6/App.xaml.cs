using Common.CommonTools.Log;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

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
            InitializeComponent();

            var mutex = new Mutex(false, "wpfDemoMutex", out bool createNew);
            if (!createNew)
            {
                MessageBox.Show("Already Exist!");
                return;
                //Application.Current.Shutdown();
            }

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
    }
}