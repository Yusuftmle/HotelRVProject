using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelRv.Infrastructure.Persistence.Context
{
    public class SeedData
    {
        private static List<User> GetUser()
        {
            var result = new Faker<User>("tr")
             // 1. Kullanıcılar için Faker
             .RuleFor(u => u.firstName, f => f.Name.FirstName())
             .RuleFor(u => u.lastName, f => f.Name.LastName())
             .RuleFor(u=>u.FullName,f=>f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password()) // Normalde hash kullanılmalı
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Role, f => f.PickRandom(new[] { "Customer", "Manager", "Admin" }))
            .Generate(20);

            return result;

        }

        public async Task seedAsync(IConfiguration configuration)
        {
            var dbcontextBuilder = new DbContextOptionsBuilder();
            dbcontextBuilder.UseSqlServer(configuration["BlazorSozlukDbConnectionStrings"]);
            var context=new HotelVRContext(dbcontextBuilder.Options);

            if (context.users.Any())
            {
                await Task.CompletedTask;
                return;
            }

            var users=GetUser();
            var userIds = users.Select(i => i.Id);
            await context.AddRangeAsync(users);
            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            int counter = 0;

            var countryFaker = new Faker<Country>()
         .RuleFor(r => r.Name, f => f.Address.Country());

            var countries = countryFaker.Generate(10);

            var cityFaker = new Faker<City>()
                .RuleFor(r => r.Name, f => f.Address.City())
                .RuleFor(r => r.PlateCode, f => f.Random.Int(1, 81).ToString()) // Türkiye plaka kodları
                .RuleFor(r => r.Country, f => f.PickRandom(countries)); // Country ilişkilendirilmesi

            var cities = cityFaker.Generate(30);

            var districtFaker = new Faker<District>()
                .RuleFor(r => r.Name, f => f.Address.StreetName())
                .RuleFor(r => r.City, f => f.PickRandom(cities)); // City ilişkilendirilmesi

            var districts = districtFaker.Generate(50);
            // 2. Oteller için Faker (Yalnızca Manager rolündeki kullanıcılarla ilişkilendirilir)
            var hotelFaker = new Faker<Hotel>()
                .RuleFor(h => h.Name, f => f.Company.CompanyName())
                .RuleFor(h => h.Address, f => f.Address.FullAddress())
                .RuleFor(h => h.Manager, f => f.PickRandom(users.Where(u => u.Role == "Manager")))
                .RuleFor(h => h.City, f => f.PickRandom(cities))
                .RuleFor(h => h.Country, f => f.PickRandom(countries))
                .RuleFor(h => h.District, f => f.PickRandom(districts));


                 var hotels = hotelFaker.Generate(30);

            // 3. Odalar için Faker
            var roomFaker = new Faker<Room>()
                .RuleFor(r => r.RoomNumber, f => f.Random.Int(1, 100).ToString()) // Oda numarası
                .RuleFor(r => r.RoomType, f => f.PickRandom(new[] { "Single", "Double", "Suite" })) // Oda türü
                .RuleFor(r => r.PricePerNight, f => f.Finance.Amount(50, 500)) // Gecelik ücret
                .RuleFor(r => r.Hotel, f => f.PickRandom(hotels)); // Otel ile ilişkilendirme

            var rooms = roomFaker.Generate(100); // 100 rastgele oda oluştur



            // 4. Rezervasyonlar için Faker
            var reservationFaker = new Faker<Reservation>()
    .RuleFor(r => r.CheckInDate, f => f.Date.Soon(30))
    .RuleFor(r => r.CheckOutDate, (f, r) => r.CheckInDate.AddDays(f.Random.Int(1, 14)))
    .RuleFor(r => r.User, f => f.PickRandom(users))
    .RuleFor(r => r.Hotel, f => f.PickRandom(hotels))
    .RuleFor(r => r.Room, f => f.PickRandom(rooms)) // Room ilişkilendirilmesi
    .RuleFor(r => r.Status, f => f.Random.ArrayElement(new[] { "Pending", "Confirmed", "Cancelled" }));

            var reservations = reservationFaker.Generate(50);


            // 5. İncelemeler için Faker
            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.Comment, f => f.Lorem.Paragraph())
                .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                .RuleFor(r => r.User, f => f.PickRandom(users))
                .RuleFor(r => r.Hotel, f => f.PickRandom(hotels));
            var reviews = reviewFaker.Generate(50);


         


            await context.AddRangeAsync(countries);
            await context.AddRangeAsync(cities);
            await context.AddRangeAsync(districts);
            await context.AddRangeAsync(hotels);
            await context.AddRangeAsync(rooms);
            await context.AddRangeAsync(reservations);
            await context.AddRangeAsync(reviews);
            await context.AddRangeAsync(users);
          
            

              await context.SaveChangesAsync();

        }


    }
}
