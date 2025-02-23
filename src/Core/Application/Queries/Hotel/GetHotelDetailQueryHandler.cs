using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using HotelVR.Common.Infrastructure.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Hotel
{
    public class GetHotelDetailQueryHandler : IRequestHandler<GetHotelDetailQuery, GetHotelDetailViewModel>
    {
        private readonly IHotelRepository hotelRepository;

        public GetHotelDetailQueryHandler(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task<GetHotelDetailViewModel> Handle(GetHotelDetailQuery request, CancellationToken cancellationToken)
        {
           var query=hotelRepository.AsQueryable();
            query = query.Include(i => i.Reservations)
              .Include(i => i.Reviews)
              .Include(i => i.Rooms);



            var list = query.Select(i => new GetHotelDetailViewModel()
            {
                Id = i.Id,
                Name = i.Name,
                Location = i.Address,
                Rooms = i.Rooms.Select(r => new RoomViewModel
                {
                    Id = r.Id,
                    RoomType = r.RoomType,
                    Price = r.PricePerNight,
                    IsAvailable = r.IsAvailable
                }).ToList(),
                Reservations = i.Reservations.Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    CustomerName = r.User.FullName
                }).ToList(),
                Reviews = i.Reviews.Select(r => new ReviewViewModel
                {
                    ReviewerName = r.User.FullName,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList(),
                Rating = i.Reviews.Average(r => r.Rating) // Ortalama puan
            });

            return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
