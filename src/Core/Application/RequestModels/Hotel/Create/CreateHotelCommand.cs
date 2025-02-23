using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.Hotel.Create
{
    public class CreateHotelCommand : IRequest<Guid>
    {
        public Guid? ManagerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }


    }
}
