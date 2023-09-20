using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
