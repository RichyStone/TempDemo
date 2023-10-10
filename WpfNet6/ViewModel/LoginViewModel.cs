using Common.CommonTools.Log;
using Common.CommonTools.McsFile.Word;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfNet6.ViewModel
{
    public partial class LoginViewModel : ObservableValidator/*, IDataErrorInfo*/
    {
        public LoginViewModel()
        {
            ErrorsChanged += LoginViewModel_ErrorsChanged;
        }

        private void LoginViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            try
            {
                errors = string.Join(Environment.NewLine, GetErrors().Where(err => !string.IsNullOrEmpty(err.ErrorMessage)));
                OnPropertyChanged(nameof(Errors));
            }
            catch (Exception ex)
            {
                LogHelper.LogError("数据验证错误：", ex);
            }
        }

        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(Error))]
        [NotifyPropertyChangedFor(nameof(TestNotify))]
        private string? userName;

        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(Error))]
        [MinLength(2, ErrorMessage = "out of range")]
        private string? password;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginFuncCommand))]
        private bool enable;

        public string TestNotify => $"Notify:{UserName}";

        [ObservableProperty]
        private double? num;

        private string validation;

        public string Validation
        {
            get => validation;
            set
            {
                if (value.Length is < 3 or > 6)
                    throw new ArgumentException("User name must be Between 3 and 6 characters long");

                SetProperty(ref validation, value);
            }
        }

        [ObservableProperty]
        private string sndValidation;

        partial void OnUserNameChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
                UserName = oldValue;
            else
                UserName = newValue;

            //OnPropertyChanged(nameof(TestNotify));
        }

        [RelayCommand(CanExecute = nameof(CanButtonClick), IncludeCancelCommand = true)]
        private async Task LoginFunc(CancellationToken cancellationToken)
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

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("MessageRecived"));

            var reply = WeakReferenceMessenger.Default.Send(new RequestMessage<string>());

            var res = reply.Response;

            WordHelper.TestMethod($"{Environment.CurrentDirectory}", "Test.docx");
        }

        private bool CanButtonClick => Enable;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0.1, 100, ErrorMessage = "Out of Range")]
        private double testValidator;

        private string errors;

        public string Errors
        {
            get => errors;
        }

        #region IDataErrorInfo

        ////public string Errors
        ////{
        ////    get
        ////    {
        ////        var list = new List<string>() {
        ////            this[nameof(UserName)],
        ////            this[nameof(Password)]
        ////        };

        ////        list.AddRange(GetErrors().Select(err =>
        ////        {
        ////            if (!string.IsNullOrEmpty(err.ErrorMessage))
        ////                return err.ErrorMessage;
        ////            else
        ////                return string.Empty;
        ////        }).Where(err => !string.IsNullOrWhiteSpace(err)));

        ////        return string.Join(Environment.NewLine, list.Where(str => !string.IsNullOrWhiteSpace(str)));
        ////    }
        ////}

        ////public string this[string columnName]
        ////{
        ////    get
        ////    {
        ////        if (columnName == nameof(UserName))
        ////        {
        ////            if (string.IsNullOrWhiteSpace(UserName))
        ////                return "UserName is required";
        ////            if (UserName.Length < 3)
        ////                return "Username must be at least 3 characters long";
        ////        }
        ////        else if (columnName == nameof(Password))
        ////        {
        ////            if (string.IsNullOrWhiteSpace(Password))
        ////                return "Password is required";
        ////            if (Password.Length < 3)
        ////                return "Password must be at least 3 characters long";
        ////        }
        ////        return string.Empty;
        ////    }
        ////}

        #endregion IDataErrorInfo
    }
}