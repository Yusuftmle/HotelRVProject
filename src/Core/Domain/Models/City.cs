using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class City:BaseEntity
    {
       
        public string Name { get; set; }
        public string PlateCode { get; set; } // İl plaka kodu
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public List<District> Districts { get; set; }
    }
}
