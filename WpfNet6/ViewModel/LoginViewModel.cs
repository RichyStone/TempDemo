using CommonTools.Excel;
using CommonTools.Log;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfNet6.Model.OtherModels;

namespace WpfNet6.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {

        }

        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(TestNotify))]
        private string? userName;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginFuncCommand))]
        private bool enable;

        public string TestNotify => $"Notify:{UserName}";

        [ObservableProperty]
        private double? num;

        partial void OnUserNameChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
                UserName = oldValue;
            else
                UserName = newValue;

            OnPropertyChanged(nameof(TestNotify));
        }

        partial void OnPasswordChanged(string? oldValue, string? newValue)
        {

            if (string.IsNullOrWhiteSpace(newValue))
                Password = oldValue;
            else
                Password = newValue;
        }

        [RelayCommand(CanExecute = nameof(CanButtonClick))]
        private async Task LoginFunc()
        {
            await Task.Delay(1000);

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名或密码为空！", "登录", MessageBoxButton.OK, MessageBoxImage.Error);
                LogHelper.LogError("登录失败");
            }

            LogHelper.LogInfo("登录成功");

            ////ExcelHelper.GetSaveFileRoute("TempData.xlsx");
            Num = 16.4E-1;
        }

        private bool CanButtonClick() => Enable;

    }
}
