using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories.Interfaces;
using HotelRv.Infrastructure.Persistence.Context;
using HotelRv.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelRv.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<HotelVRContext>(conf =>
            {
                var conStr = configuration["BlazorSozlukDbConnectionStrings"];
                conf.UseSqlServer(conStr);
            });

             var seedData = new SeedData();
            seedData.seedAsync(configuration).GetAwaiter().GetResult();

            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<ISupportTicketRepository, SupportTicketRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
