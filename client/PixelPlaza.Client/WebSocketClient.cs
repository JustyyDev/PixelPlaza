using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlaza.Client
{
    public class WebSocketClient
    {
        private ClientWebSocket ws;
        private CancellationTokenSource cts;
        public event Action<string>? OnMessageReceived;

        public WebSocketClient()
        {
            ws = new ClientWebSocket();
            cts = new CancellationTokenSource();
            ConnectAsync();
        }

        private async void ConnectAsync()
        {
            try
            {
                // Change URI as needed for your server!
                await ws.ConnectAsync(new Uri("ws://localhost:10000"), cts.Token);
                _ = ReceiveLoop();
            }
            catch (Exception ex)
            {
                OnMessageReceived?.Invoke($"[Error] {ex.Message}");
            }
        }

        public async Task SendAsync(string message)
        {
            if (ws.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await ws.SendAsync(buffer, WebSocketMessageType.Text, true, cts.Token);
            }
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[1024];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buffer, cts.Token);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    OnMessageReceived?.Invoke(msg);
                }
            }
        }
    }
}