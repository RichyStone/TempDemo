using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WpfFrameWork.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }
}