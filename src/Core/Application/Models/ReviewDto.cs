using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ReviewDto
    {
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        
    }
}
