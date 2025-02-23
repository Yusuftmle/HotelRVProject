using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User: BaseEntity
    {
        public string FullName { get; set; } // Kullanıcı tam adı
        public string Email { get; set; } // Kullanıcı e-posta adresi

       public string firstName { get; set; } // Kullanıcı adı
        public string lastName { get; set; } // Kullanıcı soyadı
        public string PasswordHash { get; set; } // Şifre hash
        public string PhoneNumber { get; set; } // Telefon numarası
        public string? Role { get; set; } // Kullanıcı rolü: Customer, Manager, Admin
         // Navigasyon Özellikleri
        public ICollection<Hotel> ManagedHotels { get; set; } // Yönettiği oteller
        public ICollection<Reservation> Reservations { get; set; } // Yaptığı rezervasyonlar
        public ICollection<Review> Reviews { get; set; } // Yaptığı incelemeler
        public ICollection<SupportTicket> SupportTickets { get; set; } // Destek talepleri



    }
}
