#define TaskScheduler
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformDemo
{
    public partial class Form1 : Form
    {
        private SynchronizationContext synchronizationContext;

        public Form1()
        {
            InitializeComponent();

            synchronizationContext = SynchronizationContext.Current;

            Task.Factory.StartNew(() =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                SetTextBoxValue(txtBoxThreadID, $"SynchronizationContext:{id}");
            }, new CancellationToken(), TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            TestCallBack();
        }

        private ManualResetEvent manualEvent = new ManualResetEvent(false);

        private void TestCallBack()
        {
            ThreadPool.RegisterWaitForSingleObject(manualEvent, CallBack, "CallBack", 3000, false);

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    manualEvent.Reset();

                    await Task.Delay(1000);

                    manualEvent.Set();
                }
            }, TaskCreationOptions.LongRunning);

        }

        private void CallBack(object obj, bool timeOut)
        {
            try
            {
#if TaskScheduler

                Task.Factory.StartNew(() =>
                {
                    var str = (string)obj;
                    var id = Thread.CurrentThread.ManagedThreadId.ToString();
                    SetTextBoxValue(txtBoxThreadID, $"{str}_{id}");
                });

#else
                var str = (string)obj;
                var id = Thread.CurrentThread.ManagedThreadId.ToString();
                synchronizationContext.Send(o =>
                {
                   SetTextBoxValue(str);
                }, id);
#endif
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private static readonly object objLock = new object();

        private void SetTextBoxValue(TextBox textBox, string str)
        {
            bool taken = false;
            try
            {
                Monitor.Enter(objLock, ref taken);
                if (taken)
                {
                    if (InvokeRequired)
                        this.Invoke(new Action(() => { textBox.Text = str; }));
                    else
                        textBox.Text = str;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"设置失败：{ex.Message}");
            }
            finally
            {
                if (taken)
                    Monitor.Exit(objLock);
            }
        }

        private void InvokeAsync(Func<bool> action)
        {
            var asyncResult = action.BeginInvoke(InvokeCallBack, new object());

            while (asyncResult != null && !asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2)))
            {
                var res = action.EndInvoke(asyncResult);
            }
        }

        private void InvokeCallBack(IAsyncResult asyncResult)
        {
            var obj = asyncResult.AsyncState;
            var res = (AsyncResult)asyncResult;
            var dele = (Func<bool>)res.AsyncDelegate;
            var result = dele.EndInvoke(asyncResult);
        }

    }
}
