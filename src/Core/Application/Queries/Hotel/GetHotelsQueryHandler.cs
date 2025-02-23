using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositories.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Queries.Hotel
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, List<HotelDto>>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public GetHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<List<HotelDto>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await hotelRepository.GetAll();
            return mapper.Map<List<HotelDto>>(hotels);

        }
    }
}
