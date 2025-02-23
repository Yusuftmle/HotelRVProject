using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelVR.Common.Infrastructure.Models.Queries;
using MediatR;

namespace Application.Queries.Hotel
{
    public class GetHotelDetailQuery:IRequest<GetHotelDetailViewModel>
    {
        public GetHotelDetailQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; set; }

    }
}
