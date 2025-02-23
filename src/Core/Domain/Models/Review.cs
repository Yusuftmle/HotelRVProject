using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review: BaseEntity
    {
        public Guid HotelId { get; set; } // İncelemenin yapıldığı otel (FK)
        public Hotel Hotel { get; set; } // Otelle ilişki
        public Guid UserId { get; set; } // İnceleme yapan kullanıcı (FK)
        public User User { get; set; } // Kullanıcıyla ilişki
        public string Comment { get; set; } // Kullanıcı yorumu
        public int Rating { get; set; } // Puanlama (1-5 arası)
        public bool IsApproved { get; set; }
    }
}
