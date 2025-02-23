using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Hotel: BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        // Mevcut propertyler

        public City City { get; set; }
        public Guid CityId { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public District District { get; set; }
        public Guid DistrictId { get; set; }

        // Adresi daha detaylı yapabiliriz
      
        public Guid? ManagerId { get; set; }
        public User Manager { get; set; }
        public ICollection<Room> Rooms { get; set; } // Otel odaları
        public ICollection<Review> Reviews { get; set; } // Otel incelemeleri
        public ICollection<Reservation> Reservations { get; set; }
    }
}
