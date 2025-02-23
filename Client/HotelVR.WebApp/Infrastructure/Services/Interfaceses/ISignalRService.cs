namespace HotelVR.WebApp.Infrastructure.Services.Interfaceses
{
    public interface ISignalRService
    {
        Task ConnectAsync();
        void OnMessageReceived(Action<string> handler);
        Task SendMessageAsync(string message);
    }
}