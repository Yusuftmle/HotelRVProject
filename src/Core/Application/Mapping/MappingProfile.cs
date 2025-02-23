using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Queries.user;
using Application.RequestModels.Hotel.Create;
using Application.RequestModels.Hotel.Update;
using Application.RequestModels.Reservatıon.Create;
using Application.RequestModels.Review.CreateRevıew;
using Application.RequestModels.Review.UpdateRevıew;
using Application.RequestModels.User.CreateUser;
using Application.RequestModels.User.UpdateUser;
using AutoMapper;
using Domain.Models;
using HotelVR.Common.Infrastructure.Models.Queries;

namespace Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<Hotel, HotelDto>();

            CreateMap<CreateUserCommand, User>()
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User")); // Role için varsayılan değer

            CreateMap<UpdateUserCommand, User>();


            CreateMap<CreateHotelCommand, Hotel>()
                 .ReverseMap();

            CreateMap<UpdateHotelCommand, Hotel>()
                 .ReverseMap();

            CreateMap<CreateReviewCommand,Review>()
                 .ReverseMap();
            CreateMap<Room,RoomDto>()
                .ReverseMap();

            CreateMap<UpdateReviewCommand, Review>();
             CreateMap<Review, ReviewDto>();

            CreateMap<CreateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID'yi otomatik set etmesin
            .ForMember(dest => dest.CreatedById, opt => opt.Ignore()) // CreatedById'yi sonra set edeceğiz
            .ForMember(dest => dest.CheckInDate, opt => opt.Ignore())
            .ForMember(dest => dest.CheckOutDate, opt => opt.Ignore());

            // Reservation -> ReservationDto Mapping
            CreateMap<Reservation, ReservationDto>();
                


        }
    }
}
