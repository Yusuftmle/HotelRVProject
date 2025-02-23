using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models.Queries
{
    public class ReviewViewModel
    {
        public DateTime createdDate { get; set; }   
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
