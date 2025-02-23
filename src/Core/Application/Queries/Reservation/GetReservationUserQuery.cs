using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using MediatR;

namespace Application.Queries.Reservation
{
    public class GetReservationUserQuery: IRequest<List<ReservationDto>>
    {
        public GetReservationUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
