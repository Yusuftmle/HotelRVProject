using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models
{
    public class PagedViewModel<T> where T : class
    {
        public PagedViewModel() : this(new List<T>(), new Page())
        {
            
        }

        public PagedViewModel(IList<T> results, Page pageInfo)
        {
            Results = results;
            PageInfo = pageInfo;
        }

        public IList<T> Results { get; set; }
        public int Count => Results?.Count ?? 0;
        public Page PageInfo { get; set; }
    }
}
