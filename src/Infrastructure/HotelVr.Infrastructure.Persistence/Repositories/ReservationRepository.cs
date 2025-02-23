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
    public class ReservationRepository : GenericRepository<Reservation>,IReservationRepository
    {
        public ReservationRepository(HotelVRContext dbContext ) : base(dbContext, dbContext.Set<Reservation>())
        {
            
        }
        public async Task<List<Reservation>> GetReservationsByUserIdAsync(Guid userId)
        {
            return await Get(r => r.UserId == userId)  // Kullanıcının rezervasyonlarını filtreliyoruz
                .Include(r => r.Room)
                .ToListAsync();  // List olarak döndürüyoruz
        }

    }
}
