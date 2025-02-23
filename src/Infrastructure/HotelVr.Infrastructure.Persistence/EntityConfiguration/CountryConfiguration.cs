using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using HotelRv.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelRv.Infrastructure.Persistence.EntityConfiguration
{
    public class CountryConfiguration:BaseEntityConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);
            builder.ToTable("Country", HotelVRContext.DEFAULT_SCHEMA);
            builder.Property(h => h.Name)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
