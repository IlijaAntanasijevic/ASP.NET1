using Domain.Core;
using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Apartment : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaxGuests { get; set; }
        public string MainImage { get; set; }
        public Decimal Price { get; set; }
        public int CityCountryId { get; set; }
        public int UserId { get; set; }
        public int ApartmentTypeId { get; set; }


        public virtual CityCountry CityCountry { get; set; }
        public virtual User User { get; set; }
        public virtual ApartmentType ApartmentType { get; set; }

        public virtual ICollection<FeatureApartment> FeatureApartments { get; set; } = new HashSet<FeatureApartment>();
        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();
        public virtual ICollection<PaymentApartment> PaymentApartments { get; set;} = new HashSet<PaymentApartment>();
        public virtual ICollection<Booking> Bookings { get; set;} = new HashSet<Booking>();

    }
}
