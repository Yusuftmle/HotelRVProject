using HotelVR.Common.Infrastructure.Models.Queries;

namespace HotelVR.WebApp.Infrastructure.Services.Interfaceses
{
    public interface IHotelRoomService
    {
        Task<bool> CheckRoomAvailability(int roomId, DateTime checkIn, DateTime checkOut);
        Task<List<RoomViewModel>> GetAllRooms();
        Task<RoomViewModel> GetRoomById(int id);
    }
}