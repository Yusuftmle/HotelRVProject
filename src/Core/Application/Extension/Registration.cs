using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extension
{
    public static class Registration
    {
        public static IServiceCollection AddAplicationRegistration(this IServiceCollection services)
        {

            var assm = Assembly.GetExecutingAssembly();


            // MediatR konfigürasyonu düzeltildi

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assm));



            // AutoMapper konfigürasyonu
            services.AddAutoMapper(assm);

            // FluentValidation için doğrulayıcıları assembly'den ekleme
            services.AddValidatorsFromAssembly(assm);

            return services;
        }
    }
}
