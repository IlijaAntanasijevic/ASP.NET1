using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FeatureApartment
    {
        public int ApartmentId { get; set; }
        public int FeatureId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Feature Feature { get; set; }   
    }
}
