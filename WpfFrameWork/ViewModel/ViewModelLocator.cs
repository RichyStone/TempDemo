using Microsoft.Extensions.DependencyInjection;
using System;

namespace WpfFrameWork.ViewModel
{
    public class ViewModelLocator
    {
        private IServiceProvider _serviceProvider;

        public ViewModelLocator()
        {
            var _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<MainViewModel>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public MainViewModel MainViewModel => _serviceProvider.GetService<MainViewModel>();
    }
}