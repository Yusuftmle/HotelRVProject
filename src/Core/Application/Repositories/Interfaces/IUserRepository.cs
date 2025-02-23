using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using HotelRv.Infrastructure.Persistence.Repositories;

namespace Application.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
