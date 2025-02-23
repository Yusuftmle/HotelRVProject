using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Queries.Room;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Queries.Room
{
   
    public class GetRoomsByHoteIdQueryHandler : IRequestHandler<GetRoomsByHoteIdQuery, List<RoomDto>>
    {
        private readonly IMapper mapper;
        private readonly IRoomRepository roomRepository;




        public GetRoomsByHoteIdQueryHandler(IMapper mapper, IRoomRepository roomRepository)
        {
            this.mapper = mapper;
            this.roomRepository = roomRepository;
        }
        public async Task<List<RoomDto>> Handle(GetRoomsByHoteIdQuery request, CancellationToken cancellationToken)
        {
            var hotels = await roomRepository.GetAll();
            return mapper.Map<List<RoomDto>>(hotels);
        }
    }
}




