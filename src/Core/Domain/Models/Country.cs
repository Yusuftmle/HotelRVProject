using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Country:BaseEntity
    {
        public string Name { get; set; } // Örneğin: "Türkiye"
        public ICollection<City> Cities { get; set; }
    }
}
