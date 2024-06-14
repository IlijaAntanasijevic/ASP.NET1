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
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasOne(x => x.Apartment)
                   .WithMany(x => x.Images)
                   .HasForeignKey(x => x.ApartmentId);
        }
    }
}
