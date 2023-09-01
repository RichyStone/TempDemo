using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfNet6.View;

namespace WaveMeter_GUI.GlobalManager
{
    public static class ViewManager
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        private static ConcurrentDictionary<ViewType, ContentControl> viewDic = new ConcurrentDictionary<ViewType, ContentControl>();

        public static ContentControl GetView(ViewType viewType)
        {
            ContentControl control = null;
            try
            {
                var intptr = FindWindow(null, viewType.ToString());
                if (intptr != IntPtr.Zero && viewDic.ContainsKey(viewType))
                    control = viewDic[viewType];
                else
                {
                    control = CreateView(viewType);
                    if (viewDic.TryRemove(viewType, out _))
                        viewDic.TryAdd(viewType, control);
                }

                return control;
            }
            catch (Exception ex)
            {
                return control;
            }
        }

        private static ContentControl CreateView(ViewType viewType)
        {
            ContentControl control = null;
            switch (viewType)
            {
                case ViewType.MainWindow:
                    control = new MainWindow();
                    break;

                case ViewType.None:
                    break;
            }

            return control;
        }

    }

}

public enum ViewType
{
    None,
    MainWindow
}
