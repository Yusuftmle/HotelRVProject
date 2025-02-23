using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Queries.Reservation
{
    public class GetReservationUserQueryHandler : IRequestHandler<GetReservationUserQuery, List<ReservationDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationUserQueryHandler(IReservationRepository reservationRepository, IMapper mapper, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<ReservationDto>> Handle(GetReservationUserQuery request, CancellationToken cancellationToken)
        {
         
            var getReservationUser= await _reservationRepository.GetReservationsByUserIdAsync(request.UserId);
            if (getReservationUser == null || !getReservationUser.Any())
            {
               throw new DataBaseValidationException("Reservation not found");
            }
            return _mapper.Map<List<ReservationDto>>(getReservationUser);
        }
    }
}
