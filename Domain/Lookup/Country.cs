using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;

namespace Domain.Lookup
{
    public class Country : BasicNamedEntity
    {
        public virtual ICollection<CityCountry> CityCountries { get; set; } = new HashSet<CityCountry>();

    }
}
