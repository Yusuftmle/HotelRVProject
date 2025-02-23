using Blazored.LocalStorage;
using HotelVR.WebApp.Infrastructure.Extensions;

namespace HotelVR.WebApp.Infrastructure.Auth
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService SyncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            SyncLocalStorageService = syncLocalStorageService;
        }


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Token'ı local storage'dan al
            var token = SyncLocalStorageService.GetToken();

            // Eğer token mevcutsa ve Authorization başlığı eklenmemişse, başlığı ekle
            if (!string.IsNullOrEmpty(token) && request.Headers.Authorization is null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
