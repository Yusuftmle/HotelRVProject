using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using MediatR;

namespace Application.Queries.Hotel
{
    public class GetHotelsQuery: IRequest<List<HotelDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Stars { get; set; }
    }
}
