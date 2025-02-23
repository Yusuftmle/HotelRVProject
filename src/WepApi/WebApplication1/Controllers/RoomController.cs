using Application.Queries.Room;
using HotelVR.Common.Infrastructure.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {

        private readonly IMediator mediator;

        public RoomController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("ByHotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
        {
            var query = new GetRoomsByHoteIdQuery(hotelId);
            var rooms = await mediator.Send(query);
            return Ok(rooms);
        }

    }
}
