using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.RequestModels.Hotel.Update
{
    public class UpdateHotelCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

    }
}
