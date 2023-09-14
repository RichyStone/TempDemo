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
        [NotifyPropertyChangedFor(nameof(TestNotify))]
        private string? userName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginFuncCommand))]
        private string? password;

        public string? TestNotify { get; set; }

        [ObservableProperty]
        private double? num;

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
                LogHelper.LogError("登录失败");
                return;
            }

            LogHelper.LogInfo("登录成功");

            Num = 16.4E-1;
            ////TestSerializer();
        }

        private void TestSerializer()
        {
            var list = new List<SerializableInfo>() {
            new SerializableInfo()
            {
                Name = "TestInfo",
                Description = "测试信息",
                IP = "127.0.0.1",
                Address = "1234567",
                City = "whereEver",
                Port = 8888,
                PostalCode = "12345",
                Region = "bbb"
            },
            new SerializableInfo()
            {
                Name = "TestInfo",
                Description = "测试信息",
                IP = "127.0.0.1",
                Address = "1234567",
                City = "whereEver",
                Port = 8888,
                PostalCode = "12345",
                Region = "ccc"
            },
            new SerializableInfo()
            {
                Name = "TestInfo",
                Description = "测试信息",
                IP = "127.0.0.1",
                Address = "1234567",
                City = "whereEver",
                Port = 8888,
                PostalCode = "12345",
                Region = "ddd"
            },
            new SerializableInfo()
            {
                Name = "TestInfo",
                Description = "测试信息",
                IP = "127.0.0.1",
                Address = "1234567",
                City = "whereEver",
                Port = 8888,
                PostalCode = "12345",
                Region = "aaa"
            },
            };

            var str = CommonTools.Serialize.XmlSerializeHelper.WriteFile(list, $"{Environment.CurrentDirectory}\\Test\\tempFile");
            if (string.IsNullOrEmpty(str))
                MessageBox.Show("写入成功！");
            else
                MessageBox.Show($"写入失败：{str}");

            var getinfo = CommonTools.Serialize.XmlSerializeHelper.ReadFile<List<SerializableInfo>>($"{Environment.CurrentDirectory}\\Test\\tempFile", ".xml", out string errormsg);
            if (string.IsNullOrEmpty(errormsg))
                MessageBox.Show("读取成功！");
            else
                MessageBox.Show($"读取失败：{errormsg}");

        }

    }
}
