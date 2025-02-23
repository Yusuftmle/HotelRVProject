using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.Review.UpdateRevıew
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Guid>
    {
        private IReviewRepository repository;
        private readonly IMapper mapper;

        public UpdateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
           var dbReview=await repository.GetByIdAsync(request.Id);
            if (dbReview == null) 
            {
                throw new DataBaseValidationException("Review not found");
            }

            mapper.Map(request, dbReview);
            var rows = await repository.UpdateAsync(dbReview);


            return dbReview.Id;
        }
    }
}
