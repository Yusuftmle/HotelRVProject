using Application.Queries.Reservation;
using Application.RequestModels.Reservatıon.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpGet("GetUserReservations/{userId}")]
        public async Task<IActionResult> GetUserReservations(Guid userId)
        {
            // Query'yi oluşturuyoruz
            var query = new GetReservationUserQuery(userId);

            // MediatR aracılığıyla handler'ı çağırıyoruz
            var reservations = await _mediator.Send(query);

         
            return Ok(reservations);
        }
    }
}
