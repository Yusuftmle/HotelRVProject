using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelVR.Common.Infrastructure.Models.Queries;

namespace Application.Queries.Hotel.HotelViewModel
{
    public class HotelViewModel
    {
        public Guid Id { get; set; }
         public string  Description { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
       
        public List<ReviewViewModel> review { get; set; }
        // Diğer genel özellikler
    }
}
