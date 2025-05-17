using App.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FavoriteApartments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }

    }
}

