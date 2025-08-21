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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message).IsRequired();
            
            builder.HasOne(x => x.Apartment)
                    .WithMany(x => x.Ratings)
                    .HasForeignKey(X => X.ApartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.ApartmentRatings)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
                    
        }
    }

    public class ApartmentRatingConfiguration : IEntityTypeConfiguration<ApartmentRating>
    {
        public void Configure(EntityTypeBuilder<ApartmentRating> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Rating)
                .WithMany(x => x.ApartmentRatings)
                .HasForeignKey(x => x.RatingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RatingType)
                    .WithMany(x => x.ApartmentRatings)
                    .HasForeignKey(x => x.RatingTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
