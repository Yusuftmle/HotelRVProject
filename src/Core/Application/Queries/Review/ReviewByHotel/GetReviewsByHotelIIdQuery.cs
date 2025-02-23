using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using MediatR;

namespace Application.Queries.Review.ReviewByHotel
{
    public class GetReviewsByHotelIIdQuery : IRequest<List<ReviewDto>>
    {
        public GetReviewsByHotelIIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; set; }


    }
}
