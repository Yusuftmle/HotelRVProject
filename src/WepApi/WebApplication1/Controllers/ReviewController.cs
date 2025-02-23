using Application.Queries.Review;
using Application.Queries.Review.ReviewByHotel;
using Application.Queries.Review.ReviewByUser;
using Application.RequestModels.Review.CreateRevıew;
using Application.RequestModels.Review.UpdateRevıew;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IMediator mediator;

        public ReviewController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var reviewId = await mediator.Send(command);
            return Ok(reviewId);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewCommand command)
        {
            var reviewId = await mediator.Send(command);
            return Ok(reviewId);
        }

        [HttpGet("reviews/{HotelId}")]
        public async Task<IActionResult> GetAllReviewsByHotelId(Guid HotelId)
        {
            var reviews = await mediator.Send(new GetReviewsByHotelIIdQuery(HotelId));
            return Ok(reviews);
        }
        [HttpGet("review/{UserId}")]
        public async Task<IActionResult> GetAllReviewsByUserId(Guid UserId)
        {
            var reviews = await mediator.Send(new GetUserReviewsQuery(UserId));
            return Ok(reviews);
        }


    }
}
