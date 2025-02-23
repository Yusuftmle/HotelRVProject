using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Queries.Review.ReviewByHotel
{
    public class GetReviewsByHotelIIdQueryHandler : IRequestHandler<GetReviewsByHotelIIdQuery, List<ReviewDto>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public GetReviewsByHotelIIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }


        Task<List<ReviewDto>> IRequestHandler<GetReviewsByHotelIIdQuery, List<ReviewDto>>.Handle(GetReviewsByHotelIIdQuery request, CancellationToken cancellationToken)
        {
            var query = reviewRepository.AsQueryable();
            var reviews = query
            .Where(i => i.HotelId == request.HotelId)
            .OrderByDescending(i => i.Rating) // Rating'e göre azalan sırada sırala
            .ToList();
            if (reviews.Count == 0)
            {
                throw new InvalidOperationException("No reviews found for the given HotelId.");
            }
            return Task.FromResult(mapper.Map<List<ReviewDto>>(reviews));
        }
    }
}
