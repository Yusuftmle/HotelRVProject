using System.Net.Http;
using System.Net.Http.Json;
using Application.Features.Command;
using Application.Queries.Hotel.HotelViewModel;
using Application.Queries.Search;
using Application.RequestModels.Hotel.Update;
using HotelVR.Common.Infrastructure.Models.Queries;
using HotelVR.WebApp.Infrastructure.Services.Interfaceses;

namespace HotelVR.WebApp.Infrastructure.Services
{
    public class HotelRoomService : IHotelRoomService
    {

        private readonly HttpClient httpClient;

        public HotelRoomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<RoomViewModel>> GetAllRooms()
        {
            var response = await httpClient.GetAsync("api/hotel/rooms");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<RoomViewModel>>();
            }
            return new List<RoomViewModel>();
        }

        public async Task<RoomViewModel> GetRoomById(int id)
        {
            var response = await httpClient.GetAsync($"api/hotel/rooms/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoomViewModel>();
            }
            return null;
        }

       
        public async Task<bool> CheckRoomAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var query = $"api/hotel/rooms/{roomId}/availability?checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}";
            var response = await httpClient.GetAsync(query);
            return response.IsSuccessStatusCode;
        }
    }
  

    

}
