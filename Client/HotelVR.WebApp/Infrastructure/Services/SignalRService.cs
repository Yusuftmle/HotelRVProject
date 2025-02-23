using HotelVR.WebApp.Infrastructure.Services.Interfaceses;
using Microsoft.AspNetCore.SignalR.Client;

namespace HotelVR.WebApp.Infrastructure.Services
{
    public class SignalRService:ISignalRService
    {
        private readonly HubConnection _hubConnection;

        public SignalRService(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public async Task ConnectAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }

        public async Task SendMessageAsync( string message)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.SendAsync("ReceiveMessage", message);
            }
        }

        public void OnMessageReceived(Action< string> handler)
        {
            _hubConnection.On("ReceiveMessage", handler);
        }
    }
}
