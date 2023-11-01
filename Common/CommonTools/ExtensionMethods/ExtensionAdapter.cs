using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonTools.ExtensionMethods
{
    public static class ExtensionAdapter
    {
        public static bool JudgeTimeOut(this Task task, int TimeOout)
        {
            var completed = Task.WhenAny(task, Task.Delay(TimeOout));
            return completed == task;
        }

        public static bool JudgeTimeOut(this Task task, TimeSpan TimeOout)
        {
            var completed = Task.WhenAny(task, Task.Delay(TimeOout));
            return completed == task;
        }
    }
}