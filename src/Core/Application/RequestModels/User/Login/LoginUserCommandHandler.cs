using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using HotelVR.Common.Infrastructure;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Application.Queries.user;

namespace Application.RequestModels.User.LoginCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMediator mediator, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetSingleAsync(i => i.Email == request.Email);

            if (dbUser == null)
                throw new DataBaseValidationException("User Not Found");

            var pass = request.Password;

            if (dbUser.PasswordHash != pass)
                throw new DataBaseValidationException("Password is wrong");

            //TODO user create islemlerinde nsonra buradaki gelen password verilerini de encryptlicez
            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.Email),
            new Claim(ClaimTypes.Name, dbUser.FullName),
            new Claim(ClaimTypes.GivenName, dbUser.firstName),
            new Claim(ClaimTypes.Surname, dbUser.lastName),
            new Claim(ClaimTypes.Role, dbUser.Role) // Kullanıcının rolü
            };
            result.Token = GenerateToken(claims);

            return result;
        }


        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims,
                                             expires: expiry,
                                             signingCredentials: creds,
                                             notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
