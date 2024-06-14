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
    public class FeatureApartmentConfiguration : IEntityTypeConfiguration<FeatureApartment>
    {
        public void Configure(EntityTypeBuilder<FeatureApartment> builder)
        {
            builder.HasKey(x => new {x.ApartmentId, x.FeatureId});

            builder.HasOne(x => x.Apartment)
                   .WithMany(x => x.FeatureApartments)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Feature)
                   .WithMany(x => x.FeatureApartments)
                   .HasForeignKey(x => x.FeatureId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
