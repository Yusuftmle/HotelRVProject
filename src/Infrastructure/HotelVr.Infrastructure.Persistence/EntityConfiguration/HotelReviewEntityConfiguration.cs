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
    public class HotelReviewEntityConfiguration:BaseEntityConfiguration<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder); // BaseEntityConfiguration'dan temel yapılandırmayı al
            builder.ToTable("HotelReview", HotelVRContext.DEFAULT_SCHEMA);
            // Alanlar için doğrulamalar
            builder.Property(hr => hr.Comment)
                   .IsRequired()
                   .HasMaxLength(1000); // Yorum, 1000 karakterle sınırlı

            builder.Property(hr => hr.Rating)
               .IsRequired()
               .HasDefaultValue(1) // Puanlama için varsayılan değer
               .HasMaxLength(1) // Rating için max uzunluk belirleme
               .HasComment("Puanlama, 1 ile 5 arasında olmalıdır");

            // Rating için CheckConstraint ekleme
            builder.HasCheckConstraint("CK_HotelReview_Rating", "Rating >= 1 AND Rating <= 5"); // 1-5 arası puan

            // İlişkiler
            builder.HasOne(hr => hr.Hotel) // Bir inceleme bir otele ait
                   .WithMany(h => h.Reviews) // Bir otel birden fazla incelemeye sahip olabilir
                   .HasForeignKey(hr => hr.HotelId)
                   .OnDelete(DeleteBehavior.Cascade); // Otel silinirse, incelemeler de silinir

            builder.HasOne(hr => hr.User) // Bir inceleme bir kullanıcıya ait
                   .WithMany(u => u.Reviews) // Bir kullanıcı birden fazla inceleme yapabilir
                   .HasForeignKey(hr => hr.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse, incelemeler de silinir
        }
    }
}
