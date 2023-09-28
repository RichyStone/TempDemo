﻿using System;
using System.Threading;
using System.Windows;

namespace WpfFrameWork
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        ////public new static App Current => (App)Application.Current;

        public App()
        {
            new Mutex(true, "StoneDemo", out bool createdNew);
            if (!createdNew)
                Environment.Exit(0);

            InitializeComponent();
        }
    }
}