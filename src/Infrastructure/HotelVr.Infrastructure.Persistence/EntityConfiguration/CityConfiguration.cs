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
    public class CityConfiguration:BaseEntityConfiguration<City> 
    {
        public class HotelEntityConfiguration : BaseEntityConfiguration<City>
        {

            public override void Configure(EntityTypeBuilder<City> builder)
            {
                base.Configure(builder);
                builder.ToTable("City", HotelVRContext.DEFAULT_SCHEMA);
                builder.Property(h => h.Name)
                       .IsRequired()
                       .HasMaxLength(200);

               
            }

        }
    }
}
