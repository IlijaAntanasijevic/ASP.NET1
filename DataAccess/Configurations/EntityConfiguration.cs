using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using App.Domain;

namespace DataAccess.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
       where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsActive).HasDefaultValue(true);


            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }

    public abstract class NamedEntityConfiguration<T> : EntityConfiguration<T>
    where T : NamedEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(x => x.Name)
                   .IsUnique();
        }
    }

    public abstract class BasicEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BasicNamedEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();

        }
    }
}
