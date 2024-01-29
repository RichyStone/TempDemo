using System.Threading;
using System.Windows;

namespace WpfNet6.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mutex = new Mutex(false, "wpfDemoMutex", out bool createNew);
            if (!createNew)
            {
                MessageBox.Show("Already Exist!");
                Application.Current.Shutdown();
                return;
            }
        }
    }
}