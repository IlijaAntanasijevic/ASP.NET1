using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;
using App.Domain;

namespace Domain.Lookup
{
    public class ApartmentType : NamedEntity
    {

        public virtual ICollection<Apartment> Apartments { get; set; } = new HashSet<Apartment>();

    }
}
