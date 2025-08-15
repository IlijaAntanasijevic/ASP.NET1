using App.Domain;
using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ApartmentRating
    {
        public int Id { get; set; }
        public int RatingId { get; set; }
        public int RatingTypeId { get; set; }
        public int StarRating { get; set; }

        public virtual Rating Rating { get; set; }
        public virtual RatingType RatingType { get; set; }
    }
}
