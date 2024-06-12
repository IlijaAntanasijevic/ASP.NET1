using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;

namespace Domain.Lookup
{
    public class Feature : NamedEntity
    {

        public virtual ICollection<FeatureApartment> FeatureApartments { get; set; }
    }
}
