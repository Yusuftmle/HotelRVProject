using Application.Features.Command;
using Application.Queries.Hotel;
using Application.Queries.Search;
using Application.RequestModels.Hotel.Update;
using Domain.Models;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : BaseController
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var query = new GetHotelByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var query = new GetHotelsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchHotelQuery searchHotelQuery)
        {
            var result = await _mediator.Send(searchHotelQuery);
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateHotel([FromBody]CreateHotelCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotelCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetHotelDetailQuery(id));

            return Ok(result);
        }

    }
}
