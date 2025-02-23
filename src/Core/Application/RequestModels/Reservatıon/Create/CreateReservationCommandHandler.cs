using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Models;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.Reservatıon.Create
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // Oda müsaitlik kontrolü
            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new Exception("Oda bulunamadı");
            // Oda müsaitlik kontrolü
            var isAvailable = await _reservationRepository.IsDateRangeAvailableAsync<Reservation>(
                r => r.RoomId == request.RoomId,
                request.StartDate,
                request.EndDate
            );

            if (!isAvailable)
            {
                throw new DataBaseValidationException("Seçilen tarih aralığında oda zaten rezerve edilmiş");
            }


           

            // AutoMapper ile mapping
            var reservation = _mapper.Map<Reservation>(request);

            return reservation.Id;

        }
    }
}
