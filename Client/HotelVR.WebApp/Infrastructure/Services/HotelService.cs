using System.Net.Http.Json;
using Application.Features.Command;
using Application.Queries.Hotel.HotelViewModel;
using Application.Queries.Search;
using Application.RequestModels.Hotel.Update;
using HotelVR.Common.Infrastructure.Models.Queries;
using HotelVR.WebApp.Infrastructure.Services.Interfaceses;

namespace HotelVR.WebApp.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient httpClient;

        public HotelService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<SearchHotelViewModel>> SearchBySubject(string searchText)
        {
            var result = await httpClient.GetFromJsonAsync<List<SearchHotelViewModel>>($"/api/hotel/Search?searchText={searchText}");
            return result;
        }
        public async Task<HotelViewModel> GetHotelByIdAsync(Guid id)
        {
            return await httpClient.GetFromJsonAsync<HotelViewModel>($"/api/Hotel/{id}");
        }

        public async Task<List<HotelViewModel>> GetAllHotelsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<HotelViewModel>>("/api/Hotel")
                   ?? new List<HotelViewModel>();
        }

        public async Task<Guid> CreateHotelAsync(CreateHotelCommand command)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Hotel/Create", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Guid> UpdateHotelAsync(UpdateHotelCommand command)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Hotel/Update", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<HotelDetailViewModel> GetHotelDetailAsync(Guid id)
        {
            return await httpClient.GetFromJsonAsync<HotelDetailViewModel>($"/api/Hotel/Detail/{id}");
        }

    }
}
