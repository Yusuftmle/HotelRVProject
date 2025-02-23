using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models.Queries
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string RoomType { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public string Description  { get; set; }
    }
}
