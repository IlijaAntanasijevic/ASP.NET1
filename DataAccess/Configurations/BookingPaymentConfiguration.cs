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
    public class BookingPaymentConfiguration : IEntityTypeConfiguration<BookingPayment>
    {
        public void Configure(EntityTypeBuilder<BookingPayment> builder)
        {
            builder.HasKey(x => new { x.BookingId, x.PaymentApartmentId });

            builder.HasOne(x => x.Booking)
                   .WithMany(x => x.BookingPayments)
                   .HasForeignKey(x => x.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PaymentApartment)
                   .WithMany(x => x.BookingPayments)
                   .HasForeignKey(x => x.PaymentApartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
