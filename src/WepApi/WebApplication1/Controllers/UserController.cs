using Application.Queries.Hotel;
using Application.RequestModels.User.CreateUser;
using Application.RequestModels.User.LoginCommand;
using Application.RequestModels.User.PasswordComment;
using Application.RequestModels.User.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
       
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody ] LoginUserCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("Change Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var guid=await mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Create User")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var guid = await mediator.Send(command);
            return Ok(guid);
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
        
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var guid = await mediator.Send(command);
            return Ok(guid);
        }

    }
}
