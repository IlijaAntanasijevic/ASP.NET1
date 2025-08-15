using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Lookup
{
    public class RatingType : BasicNamedEntity
    {
        public virtual ICollection<ApartmentRating> ApartmentRatings { get; set; } = new HashSet<ApartmentRating>();
    }
}
