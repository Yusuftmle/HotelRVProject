using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using HotelVR.Common.Infrastructure.Models.Queries;
using MediatR;

namespace Application.Queries.Room
{
    public class GetRoomsByHoteIdQuery :IRequest<List<RoomDto>>
    {
        public GetRoomsByHoteIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
            
        }

        public Guid HotelId { get; set; }

       
    }
}
