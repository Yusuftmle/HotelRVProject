using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using AutoMapper;
using HotelVR.Common.Infrastructure.Exceptions;
using MediatR;

namespace Application.RequestModels.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            if (dbUser == null) 
            {
                throw new DataBaseValidationException("User not found");
            }

            var dbEmailAddress=dbUser.Email;
           // var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0; //TODO EMAIL CONFIRMED EKLENECEK RABBIT MQ KULLANILCAK
            mapper.Map(request, dbUser);
            var rows = await userRepository.UpdateAsync(dbUser);


          //  if (emailChanged && rows > 0)
            //{
              //  var @event = new UserEmailChangedEvent()
                //{
               //     OldEmailAddress = null,
                 //   NewEmailAddress = request.EmailAddress,
               // };
                //QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName, obj: @event,
                  //                                 exchangeType: SozlukConstants.DefaultExchangeType,
                    //                               queueName: SozlukConstants.UserEmailChangedQueueName);
                //dbUser.EmailConfirmed = false;
                //await userRepository.UpdateAsync(dbUser);


            return dbUser.Id;
            }

        }
    
}
