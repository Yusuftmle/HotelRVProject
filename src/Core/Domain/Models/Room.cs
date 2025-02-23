using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Room: BaseEntity
    {
        public Guid HotelId { get; set; } // Bağlı olduğu otelin kimliği (FK)
        public Hotel Hotel { get; set; } // Otelle ilişki
        public string RoomNumber { get; set; }
        public string RoomType { get; set; } // Oda tipi (Single, Double, Suite)
        public decimal PricePerNight { get; set; } // Gecelik fiyat
        public bool IsAvailable { get; set; } // Müsaitlik durumu
        public int Capacity { get; set; } // Maksimum kapasite
        public ICollection<Reservation> Reservations { get; set; } // Oda rezervasyonları
        public virtual User? CreatedByRoomId { get; set; }
    }
}
