using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reservation: BaseEntity
    {
        public Guid RoomId { get; set; } // Rezervasyonu yapılan oda (FK)
        public Room Room { get; set; } // Odayla ilişki
        public Guid UserId { get; set; } // Rezervasyon yapan kullanıcı (FK)
        public User User { get; set; } // Kullanıcıyla ilişki
        public DateTime StartDate { get; set; } // Başlangıç tarihi
        public DateTime EndDate { get; set; } // Bitiş tarihi
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ICollection<Payment> Payments { get; set; } // Rezervasyona ait ödemeler
        public decimal TotalPrice { get; set; } // Toplam fiyat
        public string Status { get; set; } // Rezervasyon durumu (Pending, Confirmed, Cancelled)
        public Guid? CreatedById { get; set; }
        public  Hotel Hotel { get; set; } // Navigasyon özelliği
        public Guid HotelId { get; set; }
        public virtual User? CreatedBy { get; set; }
    }
}
