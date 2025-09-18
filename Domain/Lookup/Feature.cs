using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;
using App.Domain;


namespace Domain.Lookup
{
    public class Feature : NamedEntity
    {
        public string Icon { get; set; }
        public virtual ICollection<FeatureApartment> FeatureApartments { get; set; }
    }
}
