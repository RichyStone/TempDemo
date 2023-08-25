using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class Lock
    {
        private Mutex mutex = new Mutex(false, "testMutex", out bool mutexNew);

        public Semaphore semaphore = new Semaphore(2, 2, "TestSemaphore", out bool semaphoreNew);

        public SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 2);

        public AutoResetEvent autoEvent = new AutoResetEvent(false);

        public ManualResetEvent manualEvent = new ManualResetEvent(false);

        private void ReleaseAll()
        {
            mutex.ReleaseMutex();
            semaphore.Release(2);
            semaphoreSlim.Release(2);
            autoEvent.Set();
            manualEvent.Set();
        }

        private void GetResourceAll()
        {
            mutex.WaitOne();

            semaphore.WaitOne();

            semaphoreSlim.Wait();
            semaphoreSlim.WaitAsync(5000);

            autoEvent.WaitOne();
            manualEvent.WaitOne();

            manualEvent.Reset();
        }

        private static readonly object obj = new object();

        private void TestMonitor()
        {
            bool taken = false;
            try
            {
                Monitor.Enter(obj, ref taken);

            }
            finally
            {
                if (taken)
                    Monitor.Exit(taken);
            }

            //var res = Monitor.Wait(obj, 2000);
            //Monitor.PulseAll(obj);
            //Monitor.Pulse(obj);

        }

        private int testLock = 1;

        private void TestInterLock()
        {
            Interlocked.Increment(ref testLock);
            Interlocked.Decrement(ref testLock);
            Interlocked.Add(ref testLock, 3);
        }

        private void ALL()
        {
            ////lock (mutex)
            ////{


            ////}

            ////try
            ////{
            ////    Monitor.Enter(obj);

            ////    Monitor.Wait(obj);

            ////    Monitor.PulseAll(obj);
            ////}
            ////catch { }
            ////finally
            ////{
            ////    Monitor.Exit(obj);
            ////}

            ////Interlocked.Decrement(ref testLock);

            ////ReaderWriterLock readerWriterLock = new ReaderWriterLock();
            ////readerWriterLock.AcquireReaderLock(TimeSpan.FromSeconds(10));
            ////readerWriterLock.ReleaseReaderLock();
        }

    }
}
