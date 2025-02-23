using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.Hotel.Update
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Guid>
    {
        private readonly IHotelRepository hotelRepository;
        private  readonly IMapper mapper;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var dbHotel = await hotelRepository.GetByIdAsync(request.Id);
            if (dbHotel == null) 
            {
                throw new DataBaseValidationException("Hotel Not Found");
            }

            mapper.Map(request, dbHotel);


            var rows = await hotelRepository.UpdateAsync(dbHotel);

            return dbHotel.Id;
        }
    }
}
