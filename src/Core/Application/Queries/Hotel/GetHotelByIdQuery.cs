using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using MediatR;

namespace Application.Queries.Hotel
{
    public class GetHotelByIdQuery : IRequest<HotelDto>
    {
        public GetHotelByIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; set; }

    }
}
