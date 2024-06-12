using Domain.Lookup;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class CityConfiguration : BasicEntityConfiguration<City>
    {
    }

    public class CountryConfiguration : BasicEntityConfiguration<Country>
    {
    }




}
