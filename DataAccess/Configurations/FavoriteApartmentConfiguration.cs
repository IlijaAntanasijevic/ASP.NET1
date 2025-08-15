using Domain;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class FavoriteApartmentConfiguration : IEntityTypeConfiguration<FavoriteApartments>
    {
        public void Configure(EntityTypeBuilder<FavoriteApartments> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                  .WithMany(x => x.Favorites)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Apartment)
                  .WithMany(x => x.Favorites)
                  .HasForeignKey(x => x.ApartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
