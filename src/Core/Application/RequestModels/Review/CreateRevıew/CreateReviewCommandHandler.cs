using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Models;
using HotelRv.Infrastructure.Persistence.Repositories;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.Review.CreateRevıew
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IReservationRepository reservationRepository;

        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.reservationRepository = reservationRepository;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.BeginTransactionAsync();

            try
            {
                // Rezervasyon kontrolü
                var reservation = await reservationRepository.GetByIdAsync(request.ReservationId);

                if (reservation == null || reservation.UserId != request.UserId || reservation.Hotel.Id != request.HotelId)
                {
                    throw new DataBaseValidationException("Yorum yapabilmek için otelde rezervasyon yapmalısınız.");
                }

                // Review nesnesini oluştur ve map et
                var review = mapper.Map<Domain.Models.Review>(request);

                // Yorum veritabanına kaydediliyor
                await reviewRepository.AddAsync(review);

                // Değişiklikler commit ediliyor (tüm işlemleri birleştirir)
                await unitOfWork.SaveChangesAsync();

                // Transaction commit ediliyor
                await unitOfWork.CommitTransactionAsync();

                return review.Id;
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new DataBaseValidationException("yapilan islemler hatali oldugundan geri alindi ");
            }

          

        }
    }
}
