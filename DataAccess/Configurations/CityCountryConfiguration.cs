using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain;

namespace DataAccess.Configurations
{
    public class CityCountryConfiguration : IEntityTypeConfiguration<CityCountry>
    {
        public void Configure(EntityTypeBuilder<CityCountry> builder)
        {

            builder.HasOne(x => x.City)
                   .WithMany(x => x.CityCountries)
                   .HasForeignKey(x => x.CityId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Country)
                   .WithMany(x => x.CityCountries)
                   .HasForeignKey(x => x.CountryId);
        }
    }
}
