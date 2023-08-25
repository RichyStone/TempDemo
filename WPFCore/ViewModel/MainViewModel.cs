using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCore.ViewModel
{
    public class MainViewModel
    {
        private System.Windows.Threading.Dispatcher GetUiDispatcher(bool app = false)
        {
            return app ? Application.Current.Dispatcher : Application.Current.Dispatcher;
        }

    }
}
