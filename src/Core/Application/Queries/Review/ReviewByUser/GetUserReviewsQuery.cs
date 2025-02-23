using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using MediatR;

namespace Application.Queries.Review.ReviewByUser
{
    public class GetUserReviewsQuery:IRequest<List<ReviewDto>>
    {
        public GetUserReviewsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
