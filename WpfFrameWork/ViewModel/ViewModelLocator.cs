using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFrameWork.ViewModel
{
    public class ViewModelLocator
    {
        IServiceProvider _serviceProvider;

        public ViewModelLocator()
        {
            var _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton<MainViewModel>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public MainViewModel MainViewModel => _serviceProvider.GetService<MainViewModel>();

    }
}
