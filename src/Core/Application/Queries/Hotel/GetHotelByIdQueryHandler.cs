using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Models;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Hotel
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;

        public GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
        }

        public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotelId = await hotelRepository.GetByIdAsync(request.HotelId);
            
            

            if (hotelId == null)
            {
                // Otel bulunamazsa, uygun bir hata fırlatabilirsiniz
                throw new DataBaseValidationException($"Hotel with ID {request.HotelId} not found.");
            }


            // Otele ait tüm rating'leri çek
            var ratings =  reviewRepository.GetRatingsByHotelId(hotelId.Id);

            // Ortalama hesapla (rating yoksa 0 döner)
            double averageRating = ratings.Any() ? ratings.Average() : 0;

            // HotelDto'yu mapping yaparken rating bilgilerini de ekle
            var hotelDto = mapper.Map<HotelDto>(hotelId);

            hotelDto.rating = averageRating;
            return hotelDto;


        }
    }
}
