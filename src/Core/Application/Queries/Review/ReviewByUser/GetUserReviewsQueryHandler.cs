using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Queries.Review.ReviewByUser
{
    public class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, List<ReviewDto>>
    {
        public IReviewRepository reviewRepository { get; set; }
        public IMapper mapper { get; set; }
        public GetUserReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        
        public Task<List<ReviewDto>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            var query = reviewRepository.AsQueryable();
            var reviews = query
            .Where(i => i.UserId == request.UserId)
            .OrderByDescending(i => i.Rating) // Rating'e göre azalan sırada sırala
            .ToList();
            if (reviews.Count == 0)
            {
                throw new InvalidOperationException("No reviews found for the given User.");
            }
            return Task.FromResult(mapper.Map<List<ReviewDto>>(reviews));
        }
    }
}
