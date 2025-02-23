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
    public class PaymentEntityConfiguration:BaseEntityConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder); // BaseEntityConfiguration'dan temel yapılandırmayı al
            builder.ToTable("Payment", HotelVRContext.DEFAULT_SCHEMA);
            // Alanlar için doğrulamalar
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Ödenen tutar, decimal olarak tanımlanır

            builder.Property(p => p.PaymentDate)
                   .IsRequired(); // Ödeme tarihi zorunlu

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50); // Ödeme yöntemi, 50 karakterle sınırlı

            builder.Property(p => p.IsSuccessful)
                   .IsRequired(); // Ödeme başarılı mı

            // İlişkiler
            builder.HasOne(p => p.Reservation) // Bir ödeme bir rezervasyona ait
                   .WithMany(r => r.Payments) // Bir rezervasyon birden fazla ödeme alabilir
                   .HasForeignKey(p => p.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade); // Rezervasyon silinirse, ödemeler de silinir
        }
    }
}
