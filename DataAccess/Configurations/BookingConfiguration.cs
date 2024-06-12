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
    public class BookingConfiguration : EntityConfiguration<Booking>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(x => x.CheckIn)
                   .IsRequired();

            builder.Property(x => x.CheckOut)
                   .IsRequired();

            builder.HasIndex(x => new { x.CheckIn, x.CheckOut, x.ApartmentId });

            builder.Property(x => x.TotalPrice).HasColumnType("decimal(10,2)");

            builder.Property(x => x.TotalGuests).HasMaxLength(10);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Bookings)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Apartment)
                   .WithMany(x => x.Bookings)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
