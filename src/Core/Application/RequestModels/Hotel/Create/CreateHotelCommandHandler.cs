using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.Hotel.Create
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var existHotel = await hotelRepository.GetSingleAsync(i => i.Name == request.Name);
            if (existHotel != null)
            {
                throw new DataBaseValidationException("Hotel Already Exist");
            }

            var guidManager = request.ManagerId;
            guidManager = Guid.NewGuid();
            var dbHotel = mapper.Map<Domain.Models.Hotel>(request);

            var rows = await hotelRepository.AddAsync(dbHotel);


            return guidManager.Value;

        }
    }
}
