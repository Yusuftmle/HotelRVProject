using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment:BaseEntity
    {
        public Guid ReservationId { get; set; } // İlgili rezervasyon kimliği (FK)
        public Reservation Reservation { get; set; } // Rezervasyonla ilişki
        public decimal Amount { get; set; } // Ödenen tutar
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public string PaymentMethod { get; set; } // Ödeme yöntemi (Credit Card, PayPal, etc.)
        public bool IsSuccessful { get; set; } // Başarı durumu
        
    }
}
