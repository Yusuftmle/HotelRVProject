using Application.Features.Command;
using Application.Queries.Hotel.HotelViewModel;
using Application.Queries.Search;
using Application.RequestModels.Hotel.Update;

namespace HotelVR.WebApp.Infrastructure.Services.Interfaceses
{
    public interface IHotelService
    {
        Task<Guid> CreateHotelAsync(CreateHotelCommand command);
        Task<List<HotelViewModel>> GetAllHotelsAsync();
        Task<HotelViewModel> GetHotelByIdAsync(Guid id);
        Task<HotelDetailViewModel> GetHotelDetailAsync(Guid id);
        Task<List<SearchHotelViewModel>> SearchBySubject(string searchText);
        Task<Guid> UpdateHotelAsync(UpdateHotelCommand command);
    }
}