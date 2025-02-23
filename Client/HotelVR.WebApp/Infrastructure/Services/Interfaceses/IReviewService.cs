using Application.RequestModels.Review.CreateRevıew;
using Application.RequestModels.Review.UpdateRevıew;
using HotelVR.Common.Infrastructure.Models.Queries;

namespace HotelVR.WebApp.Infrastructure.Services.Interfaceses
{
    public interface IReviewService
    {
        Task<ReviewViewModel> CreateReviewAsync(CreateReviewCommand command);
        Task DeleteReviewAsync(Guid reviewId);
        Task<List<ReviewViewModel>> GetHotelReviewsAsync(Guid hotelId);
        Task<List<ReviewViewModel>> GetUserReviewsAsync();
        Task UpdateReviewAsync(UpdateReviewCommand command);
    }
}