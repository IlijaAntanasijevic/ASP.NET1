using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CityCountry
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; } = new HashSet<Apartment>();
    }
}
