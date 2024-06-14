using Domain.Lookup;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain;

namespace DataAccess.Configurations
{
    public class ApartmentTypeConfiguration : NamedEntityConfiguration<ApartmentType>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ApartmentType> builder)
        {

        }
    }

    public class FeatureTypeConfiguration : NamedEntityConfiguration<Feature>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Feature> builder)
        {
            
        }
    }
}
