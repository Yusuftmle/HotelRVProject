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
    public  class SupportTicketEntityConfiguration : BaseEntityConfiguration<SupportTicket> 
    {
      
        public override void Configure(EntityTypeBuilder<SupportTicket> builder)
        {
            base.Configure(builder);
            builder.ToTable("SupportTicket", HotelVRContext.DEFAULT_SCHEMA);
            // Alanlar için doğrulamalar
            builder.Property(st => st.Subject)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(st => st.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(st => st.Status)
                   .IsRequired()
                   .HasMaxLength(50);

            // İlişkiler
            builder.HasOne(st => st.User) // Bir ticket bir kullanıcıya ait
                   .WithMany(u => u.SupportTickets) // Bir kullanıcı birden fazla ticket açabilir
                   .HasForeignKey(st => st.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse, ticket'lar da silinir
        }
    }
}
