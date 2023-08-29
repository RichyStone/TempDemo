using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using WpfCore.ViewModel;

namespace WpfCore
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
            UIDispatcher = Application.Current.Dispatcher;
        }

    }
}
