﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.Review.CreateRevıew
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
        public Guid ReservationId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
