using CommonTools.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfNet6.View;

namespace WaveMeter_GUI.GlobalManager
{
    public static class ViewManager
    {

        /// <summary>
        /// 窗口字典
        /// </summary>
        private static readonly ConcurrentDictionary<WindowType, Window> windowDic = new();

        /// <summary>
        /// 控件字典
        /// </summary>
        private static readonly ConcurrentDictionary<ControlType, ContentControl> controlDic = new();

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="showDialog"></param>
        public static void ShowWindow(WindowType viewType, bool showDialog = false)
        {
            var win = GetWindow(viewType);
            if (win is Window window)
            {
                if (!window.IsActive)
                    window.Activate();

                if (showDialog)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        /// <param name="viewType"></param>
        public static void HideWindow(WindowType viewType)
        {
            var win = GetWindow(viewType);
            if (win is Window window)
            {
                window.Hide();
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string? className, string windowName);

        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <param name="viewType">控件类型</param>
        /// <returns></returns>
        private static Window? GetWindow(WindowType viewType)
        {
            Window? window = null;
            try
            {
                var intptr = FindWindow(null, viewType.ToString());
                if (!windowDic.ContainsKey(viewType) || intptr == IntPtr.Zero)
                {
                    window = CreateWindow(viewType);
                    if (window != null && windowDic.TryRemove(viewType, out _))
                        windowDic.TryAdd(viewType, window);
                }
                else
                {
                    window = windowDic[viewType];
                }

                return window;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                return window;
            }
        }

        /// <summary>
        /// 创建窗口
        /// </summary>
        /// <param name="viewType">空间类型</param>
        /// <returns></returns>
        private static Window? CreateWindow(WindowType viewType)
        {
            Window? window = null;
            switch (viewType)
            {
                case WindowType.MainWindow:
                    window = new MainWindow();
                    break;

                case WindowType.None:
                    break;
            }

            return window;
        }

        /// <summary>
        /// 获取UserControl
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public static ContentControl? GetUserControl(ControlType controlType)
        {
            ContentControl? control = null;
            try
            {
                if (controlDic.ContainsKey(controlType))
                    control = controlDic[controlType];
                else
                {
                    control = CreateControl(controlType);
                    if (control != null && controlDic.TryRemove(controlType, out _))
                        controlDic.TryAdd(controlType, control);
                }

                return control;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message, ex);
                return control;
            }
        }

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        private static ContentControl? CreateControl(ControlType controlType)
        {
            ContentControl? control = null;
            switch (controlType)
            {
                case ControlType.None:
                    break;
            }

            return control;
        }

    }

    /// <summary>
    /// 控件类型
    /// </summary>
    public enum WindowType
    {
        None,
        MainWindow
    }

    public enum ControlType
    {
        None,

    }

}