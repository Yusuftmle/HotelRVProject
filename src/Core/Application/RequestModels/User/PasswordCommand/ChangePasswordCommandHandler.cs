using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.User.PasswordComment
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public ChangePasswordCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));   

            var dbUser=await userRepository.GetByIdAsync(request.UserId.Value);
            var oldPasswrod = request.oldPassword;

            if(dbUser.PasswordHash!=oldPasswrod)
                throw new DataBaseValidationException("old password is wrong");

            dbUser.PasswordHash = request.newPassword;

            await userRepository.UpdateAsync(dbUser);
            return true;

        }
    }
}
