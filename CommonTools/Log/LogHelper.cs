using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommonTools.Log
{
    public static class LogHelper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        private static bool initFlag = false;

        private static readonly object initLock = new object();

        private static void LogInit()
        {
            if (initFlag) return;

            lock (initLock)
            {
                if (!initFlag)
                    initFlag = LoadLogConfig();
            }
        }

        private static bool LoadLogConfig()
        {
            try
            {
                var directories = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}", "*.config", SearchOption.AllDirectories);
                var path = directories.FirstOrDefault(d => d.Contains("log4net"));
                if (string.IsNullOrEmpty(path))
                    return false;

                var collection = log4net.Config.XmlConfigurator.Configure(new FileInfo($"{path}"));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void LogInfo(string info)
        {
            LogInit();

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void LogDebug(string info)
        {
            LogInit();

            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(info);
            }
        }

        public static void LogError(string error)
        {
            LogInit();

            if (logerror.IsErrorEnabled)
            {
                logerror.Error(error);
            }
        }

        public static void LogError(string info, Exception ex)
        {
            LogInit();

            Console.WriteLine(info);
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, ex);
            }
        }

    }

}
