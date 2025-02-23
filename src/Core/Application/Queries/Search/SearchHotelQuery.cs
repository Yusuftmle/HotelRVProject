using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.Search
{
    public class SearchHotelQuery:IRequest<List< SearchHotelViewModel>>
    {
        public SearchHotelQuery()
        {
            
        }
        public SearchHotelQuery(string searchText)
        {
            SearchText = searchText;
        }

        public string SearchText { get; set; }
    }
}
