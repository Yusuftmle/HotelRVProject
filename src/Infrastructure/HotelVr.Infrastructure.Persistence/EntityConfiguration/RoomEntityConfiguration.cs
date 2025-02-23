using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelRv.Infrastructure.Persistence.Context;

namespace HotelRv.Infrastructure.Persistence.EntityConfiguration
{
    public class RoomEntityConfiguration:BaseEntityConfiguration<Room>
    {

        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);
            builder.ToTable("Room", HotelVRContext.DEFAULT_SCHEMA);
            builder.Property(r => r.RoomNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(r => r.PricePerNight)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(r => r.Hotel)
                   .WithMany(h => h.Rooms)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade); // Otel silinirse odalar da silinir
        }
    }
}
