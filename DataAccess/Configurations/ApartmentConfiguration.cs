using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class ApartmentConfiguration : EntityConfiguration<Apartment>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.HasIndex(x => new { x.Name, x.Price, x.MaxGuests, x.CityCountryId});

            builder.Property(x => x.Address)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.MaxGuests).HasMaxLength(10);

            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.CityCountry)
                   .WithMany(x => x.Apartments)
                   .HasForeignKey(x => x.CityCountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Apartments)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ApartmentType)
                   .WithMany(x => x.Apartments)
                   .HasForeignKey(x => x.ApartmentTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
