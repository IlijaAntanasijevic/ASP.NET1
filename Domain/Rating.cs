using App.Domain;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<ApartmentRating> Ratings { get; set; }
    }
}
