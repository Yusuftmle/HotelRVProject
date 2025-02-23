using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class District: BaseEntity
    {
      
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
