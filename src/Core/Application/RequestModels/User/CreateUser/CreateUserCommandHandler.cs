using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain.Models;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await userRepository.GetSingleAsync(i => i.Email == request.EmailAddress);

            if (existUser is not  null) 
            {
                throw new DataBaseValidationException("User already exist");
            }

            var pass= request.Password;

            var fullName = request.FirstName + request.LastName;
            
            var dbUser = mapper.Map<Domain.Models.User>(request);
            dbUser.FullName = fullName;

            dbUser.PasswordHash = pass;

           
            
           

            var rows=await userRepository.AddAsync(dbUser);

            return dbUser.Id;

        }
    }
}
