using System.Net.Http.Json;
using System.Text.Json;
using Application.RequestModels.User.LoginCommand;
using Blazored.LocalStorage;
using HotelVR.Common.Infrastructure.Exceptions;
using HotelVR.Common.Results;
using HotelVR.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using HotelVR.WebApp.Infrastructure.Auth;
using HotelVR.WebApp.Infrastructure.Services.Interfaceses;
using Application.Queries.user;

namespace HotelVR.WebApp.Infrastructure.Services
{
    public class identityService :IidentityService
    {
        // JSON serileştirme ayarları
        private JsonSerializerOptions defaultJsonOpt => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public identityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.syncLocalStorageService = syncLocalStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetToken();
        }

        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await httpClient.PostAsJsonAsync("api/User/Login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr, defaultJsonOpt);
                    responseStr = validation.FlattenErrors;
                    throw new DataBaseValidationException(responseStr);
                }

                return false;
            }


            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token)) // login success
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUsername(response.LastName);
                syncLocalStorageService.SetUserId(response.Id);

                ((AuthStateProvider)authenticationStateProvider).NotifyUserLogin(response.LastName, response.Id);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.Token);

                return true;
            }
            else
            {
                // BadRequest dışındaki hatalar için loglama veya hata fırlatma
                responseStr = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"Login başarısız oldu, durum kodu: {httpResponse.StatusCode}, Yanıt: {responseStr}");
            }


        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

            ((AuthStateProvider)authenticationStateProvider).NotifyUserLogout();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }


}
