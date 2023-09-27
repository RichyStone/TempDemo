using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace WpfNet6.ViewModel
{
    public class ViewModelLocator
    {
        private IServiceProvider? _serviceProvider;

        public ViewModelLocator()
        {
            var _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<MainViewModel>();
            _serviceCollection.AddTransient<LoginViewModel>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public MainViewModel MainViewModel => _serviceProvider.GetService<MainViewModel>();

        public LoginViewModel LoginViewModel => _serviceProvider.GetService<LoginViewModel>();

    }
}
