using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Events
{
    public class IsHaveHotelReservationEventArgs
    {
        public Guid? HotelId { get; set; }
        public bool IsReserved { get; set; }
    }
}
