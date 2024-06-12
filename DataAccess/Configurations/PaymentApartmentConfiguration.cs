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
    public class PaymentApartmentConfiguration : EntityConfiguration<PaymentApartment>
    {
       
        protected override void ConfigureEntity(EntityTypeBuilder<PaymentApartment> builder)
        {
            builder.HasIndex(x => new { x.ApartmentId, x.PaymentId });

            builder.HasOne(x => x.Apartment)
                   .WithMany(x => x.PaymentApartments)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Payment)
                   .WithMany(x => x.PaymentApartments)
                   .HasForeignKey(x => x.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
