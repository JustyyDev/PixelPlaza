using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;

namespace PixelPlaza.Client
{
    public partial class MainWindow : Window
    {
        private WebSocketClient wsClient;
        private ObservableCollection<string> messages = new();

        public MainWindow()
        {
            InitializeComponent();
            ChatList.Items = messages;
            wsClient = new WebSocketClient();
            wsClient.OnMessageReceived += msg => Dispatcher.UIThread.InvokeAsync(() => messages.Add(msg));
            SendButton.Click += SendButton_Click;
        }

        private async void SendButton_Click(object? sender, RoutedEventArgs e)
        {
            var text = ChatInput.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                await wsClient.SendAsync(text);
                ChatInput.Text = "";
            }
        }
    }
}