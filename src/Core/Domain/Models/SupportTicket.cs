using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SupportTicket:BaseEntity
    {
        public Guid UserId { get; set; } // Talep yapan kullanıcı (FK)
        public User User { get; set; } // Kullanıcıyla ilişki
        public string Subject { get; set; } // Talep konusu
        public string Message { get; set; } // Talep detayı
        public string Status { get; set; } // Durum (Open, In Progress, Closed)
    }
}
