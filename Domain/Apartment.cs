using Domain;
using Domain.Core;
using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class Apartment : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        //public int MaxGuests { get; set; }
        public string MainImage { get; set; }
        public Decimal Price { get; set; }
        public int CityCountryId { get; set; }
        public int UserId { get; set; }
        public int ApartmentTypeId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Lattitude { get; set; }
        public bool? IsArchived { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }


        public virtual CityCountry CityCountry { get; set; }
        public virtual User User { get; set; }
        public virtual ApartmentType ApartmentType { get; set; }

        public virtual ICollection<FeatureApartment> FeatureApartments { get; set; } = new HashSet<FeatureApartment>();
        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();
        public virtual ICollection<PaymentApartment> PaymentApartments { get; set;} = new HashSet<PaymentApartment>();
        public virtual ICollection<Booking> Bookings { get; set;} = new HashSet<Booking>();
        public virtual ICollection<FavoriteApartments> Favorites { get; set;} = new HashSet<FavoriteApartments>();
        public virtual ICollection<Rating> Ratings { get; set;} = new HashSet<Rating>();

    }
}
