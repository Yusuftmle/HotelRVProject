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
    public class ReservationEntityConfiguration:BaseEntityConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {

            base.Configure(builder);

            // Tablo adını "entryfavorite" olarak ayarla ve varsayılan şema kullan
            builder.ToTable("Reservation", HotelVRContext.DEFAULT_SCHEMA);
            // Alanlar için doğrulamalar
            builder.Property(r => r.StartDate)
                   .IsRequired();

            builder.Property(r => r.EndDate)
                   .IsRequired();

            builder.Property(r => r.CheckInDate)
                   .IsRequired();

            builder.Property(r => r.CheckOutDate)
                   .IsRequired();

            builder.Property(r => r.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            // İlişkiler
            builder.HasOne(r => r.Room)
                   .WithMany(room => room.Reservations)
                   .HasForeignKey(r => r.RoomId)
                   .OnDelete(DeleteBehavior.Restrict); // Oda silindiğinde rezervasyon silinmez

            builder.HasOne(r => r.User)
                   .WithMany(user => user.Reservations)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde rezervasyon da silinir

            // CreatedBy ilişkisi
        builder.HasOne(r => r.CreatedBy) // CreatedBy navigasyon özelliği
               .WithMany() // Ters navigation yok
               .HasForeignKey(r => r.CreatedById) // Yabancı anahtar
               .OnDelete(DeleteBehavior.Restrict); // Admin silindiğinde, CreatedById null olur
        }
    }
    
}
