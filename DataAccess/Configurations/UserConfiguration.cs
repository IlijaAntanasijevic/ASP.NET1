using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain;

namespace DataAccess.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Avatar)
                   .IsRequired()
                   .HasMaxLength(100);
         
            builder.HasIndex(x => new { x.Email, x.Password });

        }
    }
}
