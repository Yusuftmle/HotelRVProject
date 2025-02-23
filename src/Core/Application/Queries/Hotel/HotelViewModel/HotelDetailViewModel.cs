using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelVR.Common.Infrastructure.Models.Queries;

namespace Application.Queries.Hotel.HotelViewModel
{
    public class HotelDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        
        // Detaylı özellikler
    }
}
