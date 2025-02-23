using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using HotelVR.Common.Infrastructure.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Search
{
    public class SearchHotelQueryHandler : IRequestHandler<SearchHotelQuery, List<SearchHotelViewModel>>
    {
        private readonly IHotelRepository hotelRepository;

        public SearchHotelQueryHandler(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task<List<SearchHotelViewModel>> Handle(SearchHotelQuery request, CancellationToken cancellationToken)
        {
            var result = hotelRepository
            .Get(i => EF.Functions.Like(i.Name, $"{request.SearchText}%"))
            .Select(i => new SearchHotelViewModel ()
            {
                 Id  = i.Id,
                Subject = i.Name,
            });

            return await result.ToListAsync(cancellationToken);


        }
    }   
}
