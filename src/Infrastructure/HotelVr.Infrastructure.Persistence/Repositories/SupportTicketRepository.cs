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
    public class SupportTicketRepository : GenericRepository<SupportTicket>,ISupportTicketRepository
    {
        public SupportTicketRepository(HotelVRContext dbContext) : base(dbContext, dbContext.Set<SupportTicket>())
        {
        }
    }
}
