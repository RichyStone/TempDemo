using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows;

namespace WpfNet6.ViewModel
{
    public partial class MainViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<string>>, IRecipient<RequestMessage<string>>
    {
        public MainViewModel()
        {
            ////必须改为true才能接收消息
            IsActive = true;
        }

        private System.Windows.Threading.Dispatcher GetUiDispatcher(bool app = false)
        {
            return app ? Application.Current.Dispatcher : Application.Current.Dispatcher;
        }

        [ObservableProperty]
        private string? reciveMessage;

        public void Receive(ValueChangedMessage<string> message)
        {
            ReciveMessage = message.Value;
        }

        [ObservableProperty]
        private string? request;

        public void Receive(RequestMessage<string> message)
        {
            message.Reply("reply");
            Request = message.Response;
        }
    }
}