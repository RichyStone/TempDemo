namespace AsyncDemo
{
    public class AsyncTest
    {
        #region 调用

        private WaitHandle waitHandle = new ManualResetEvent(false);

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private void UsingThread()
        {
            Thread thread = new Thread(() => { });
            thread.IsBackground = true;
            thread.Start();

            thread.Join();
        }

        private void UsingThreadPool()
        {
            ThreadPool.QueueUserWorkItem(o => { });
            ThreadPool.RegisterWaitForSingleObject(waitHandle, (o, timeOut) => { }, null, -1, true);
        }

        private static void InitialSynchronizationContext()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        private void UsingTask()
        {
            var res = Task.Run(() => { return 1; });
            InitialSynchronizationContext();

            Task.Factory.StartNew(async () =>
              {
                  if (cancellationTokenSource.IsCancellationRequested) return;

                  await Task.Delay(5000);
              }, cancellationTokenSource.Token, TaskCreationOptions.DenyChildAttach, TaskScheduler.FromCurrentSynchronizationContext()).
              ContinueWith(delegate { });

            Task.Yield();
            Thread.Yield();

            var task = new Task(() => { });
            task.RunSynchronously();
        }

        private void CancelTask()
        {
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
            cancellationTokenSource.Dispose();
        }

        private TaskScheduler GetScheduler(bool isExclusive = false)
        {
            ConcurrentExclusiveSchedulerPair pair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, 8);
            var concurrent = pair.ConcurrentScheduler;
            var exclusive = pair.ExclusiveScheduler;

            return isExclusive ? exclusive : concurrent;
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
        }

        private void ReadAsync()
        {
            using (var fileStream = new FileStream("D://", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, bufferSize: 500, true))
            {
                var bytes = new byte[1024];
                fileStream.BeginRead(bytes, 0, 1024, ReadCallback, fileStream);
            }
        }

        private static void ReadCallback(IAsyncResult result)
        {
            //结束异步写入
            FileStream? stream = result.AsyncState as FileStream;
            stream?.EndWrite(result);
            stream?.Close();
        }

        private static void UsingParallel()
        {
            Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 5 }, (i, loop) =>
            {
                if (i == 50)
                    loop.Stop();
            });

            //Parallel.ForEach()

            Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 5 }, () => { }, () => { });

        }

        private static void PLinq(IEnumerable<int> ints)
        {
            ints.AsParallel().AsOrdered().ForAll(i => { });

            ints.AsParallel();
        }

        #endregion

        #region 读写锁

        private static readonly ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim();

        private const string filePath = "D://Log//Test.txt";

        private static int executeTimes = 0;

        private static int excuteIndex = 0;

        public static void TestReaderWriterLock()
        {
            var ret = OpenOrCreatFile(filePath);
            if (!ret) return;

            Repeat:

            Thread writeThread1 = new Thread(() => Write(filePath)) { IsBackground = true };
            writeThread1.Start();

            Parallel.For(0, 100, i =>
            {
                Read(filePath, i);
            });

            Parallel.For(100, 200, i =>
            {
                Read(filePath, i);
            });

            Parallel.For(200, 300, i =>
            {
                Read(filePath, i);
            });

            Thread writeThread2 = new Thread(() => Write(filePath)) { IsBackground = true };
            writeThread2.Start();

            Thread writeThread3 = new Thread(() => Write(filePath)) { IsBackground = true };
            writeThread3.Start();

            executeTimes++;

            if (executeTimes < 2)
                goto Repeat;
        }

        private static bool OpenOrCreatFile(string filePath)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(dir)) return false;

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                ////创建后必须关闭流，保证文件不被占用
                if (!File.Exists(filePath))
                    File.Create(filePath).Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void Write(string filePath)
        {
            try
            {
                while (!readWriteLock.TryEnterUpgradeableReadLock(200)) { }

                readWriteLock.EnterWriteLock();

                for (int i = 0; i < 100; i++)
                {
                    File.AppendAllText(filePath, $"ThreadID: {Thread.CurrentThread.ManagedThreadId}  Num: {i} DateTime: {DateTime.Now}\n");
                }

                readWriteLock.ExitWriteLock();

            }
            catch (Exception ex)
            {
                File.AppendAllText(filePath, $"错误：{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                readWriteLock.ExitUpgradeableReadLock();
            }
        }

        private static void Read(string filePath, int index)
        {
            try
            {
                readWriteLock.EnterReadLock();
                var str = File.ReadAllLines(filePath);

                Console.ForegroundColor = ConsoleColor.DarkCyan;

                if (str.Count() > 0)
                {
                    var text = str.Count() > index ? str[index] : str.Last();
                    var ii = Interlocked.Increment(ref excuteIndex);
                    var id = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine($"{text}_Index:{ii}_CurrentThreadID:{id}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                readWriteLock.ExitReadLock();
            }
        }

        #endregion

    }
}
