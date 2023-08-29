using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCore.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {

        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TestNotify))]
        private string? userName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginFuncCommand))]
        private string? password;

        public string? TestNotify { get; set; }

        partial void OnUserNameChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
                UserName = oldValue;
            else
                UserName = newValue;
        }

        partial void OnPasswordChanged(string? oldValue, string? newValue)
        {

            if (string.IsNullOrWhiteSpace(newValue))
                Password = oldValue;
            else
                Password = newValue;
        }

        [RelayCommand]
        private void LoginFunc()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名或密码为空！", "登录", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


    }
}
