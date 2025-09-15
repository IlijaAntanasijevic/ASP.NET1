using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;
using App.Domain;


namespace Domain.Lookup
{
    public class Country : BasicNamedEntity
    {
        public string Currency {  get; set; }
        public virtual ICollection<CityCountry> CityCountries { get; set; } = new HashSet<CityCountry>();

    }
}
