using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        public int RoomNumber { get; set; }
    }
}
