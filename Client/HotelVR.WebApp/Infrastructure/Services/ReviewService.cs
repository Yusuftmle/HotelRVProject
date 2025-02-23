using Application.RequestModels.Review.CreateRevıew;
using Application.RequestModels.Review.UpdateRevıew;
using HotelVR.Common.Infrastructure.Models.Queries;
using HotelVR.WebApp.Infrastructure.Services.Interfaceses;
using System.Net.Http.Json;

namespace HotelVR.WebApp.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public ReviewService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<ReviewViewModel>> GetUserReviewsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ReviewViewModel>>("/api/reviews/user/reviews");
                return response ?? new List<ReviewViewModel>();
            }
            catch (HttpRequestException ex)
            {
                // Eğer hiç yorum yoksa boş liste döndür
                if (ex.StatusCode == System.Net.HttpStatusCode.NoContent ||
                    ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<ReviewViewModel>();
                }

                // Diğer hatalar için yeniden fırlat
                throw;
            }
        }

        public async Task<List<ReviewViewModel>> GetHotelReviewsAsync(Guid hotelId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<ReviewViewModel>>($"/api/reviews/reviews/{hotelId}");
            return response ?? new List<ReviewViewModel>();
        }

        public async Task<ReviewViewModel> CreateReviewAsync(CreateReviewCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/reviews/Create", command);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReviewViewModel>();
        }

        public async Task UpdateReviewAsync(UpdateReviewCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/reviews/Update", command);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteReviewAsync(Guid reviewId)
        {
            var response = await _httpClient.DeleteAsync($"/api/reviews/{reviewId}");
            response.EnsureSuccessStatusCode();
        }
    }
}

