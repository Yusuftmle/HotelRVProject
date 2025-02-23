using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using Domain.Models;
using HotelRv.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelRv.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>,IReviewRepository
    {

        public ReviewRepository(HotelVRContext dbContext) : base(dbContext, dbContext.Set<Review>())
        {
           
        }
        public IEnumerable<int> GetRatingsByHotelId(Guid hotelId)
        {
            return Get(r => r.HotelId == hotelId)
                .Select(r => r.Rating)
                .ToList();
        }

    }
}
