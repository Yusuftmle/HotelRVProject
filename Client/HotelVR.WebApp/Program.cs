using Blazored.LocalStorage;
using HotelVR.WebApp.Infrastructure.Auth;
using HotelVR.WebApp.Infrastructure.Services;
using HotelVR.WebApp.Infrastructure.Services.Interfaceses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

namespace HotelVR.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000") // API'nýn çalýþtýðý gerçek adres

            });
           

            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<AuthTokenHandler>();
            builder.Services.AddTransient<IHotelRoomService, HotelRoomService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<identityService>();
            builder.Services.AddScoped<IidentityService, identityService>();
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
           

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            // SignalR Hub baðlantýsýný ekleyelim
            builder.Services.AddSingleton( static sp =>
                new HubConnectionBuilder()
                    .WithUrl("http://localhost:5000/notifications") // API'deki Hub URL'si
                    .WithAutomaticReconnect() // Baðlantý koparsa otomatik baðlanýr
                    .Build());
            
            builder.Services.AddBlazoredLocalStorage();
           

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
