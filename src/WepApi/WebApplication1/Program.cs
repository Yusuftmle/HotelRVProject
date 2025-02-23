using Application.Extension;
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelApi.Infrastructure;
using HotelApi.Infrastructure.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using HotelRv.Infrastructure.Persistence.Extensions;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddFluentValidation()
                .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
           
          

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAplicationRegistration();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureRegistration(builder.Configuration);
            builder.Services.ConfigureAuth(builder.Configuration);
            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.ConfigureExceptionHandling(
                includeExceptionsDetails: builder.Environment.IsDevelopment(),
                 useDefaultHandlingResponse: true);
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
           
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHub<NotificationHub>("/notifications");

            app.MapControllers();
            app.UseResponseCompression();
            app.Run();
        }
    }
}
