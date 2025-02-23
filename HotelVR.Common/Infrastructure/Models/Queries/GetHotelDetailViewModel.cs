using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models.Queries
{
    public class GetHotelDetailViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public List<RoomViewModel> Rooms { get; set; }
        public List<ReservationViewModel> Reservations { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
        
        public double Rating { get; set; } // Ortalama puan
    }
}
