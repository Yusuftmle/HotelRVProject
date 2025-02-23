using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models
{
    public class BasePagedQuery
    {

        public BasePagedQuery(int page, int pageSize)
        {
            Page = page;
            this.pageSize = pageSize;
        }

        public int Page { get; set; }
        public int pageSize { get; set; }
    }
}
