using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelRv.Infrastructure.Persistence.Context;
using System.Reflection.Emit;

namespace HotelRv.Infrastructure.Persistence.EntityConfiguration
{
    public class HotelEntityConfiguration:BaseEntityConfiguration<Hotel>
    {

        public override void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.Configure(builder);
            builder.ToTable("Hotel", HotelVRContext.DEFAULT_SCHEMA);
            builder.Property(h => h.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(h => h.Address)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(h => h.Manager)
                   .WithMany(u => u.ManagedHotels)
                   .HasForeignKey(h => h.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict); // Manager silinirse otel etkilenmesin


            // CityId ile ilişkisi
            builder.HasOne(h => h.City)
                .WithMany()  // City tablosunda ilişkiyi tutan bir koleksiyon yoksa
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Restrict); // Delete işleminde nasıl davranılacağı

            // CountryId ile ilişkisi
            builder.HasOne(h => h.Country)
                .WithMany()  // Country tablosunda ilişkiyi tutan bir koleksiyon yoksa
                .HasForeignKey(h => h.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // Delete işleminde nasıl davranılacağı

            // DistrictId ile ilişkisi
            builder.HasOne(h => h.District)
                .WithMany()  // District tablosunda ilişkiyi tutan bir koleksiyon yoksa
                .HasForeignKey(h => h.DistrictId)
                .OnDelete(DeleteBehavior.Restrict); // Delete işleminde nasıl davranılacağı
        }

    }
}
