using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class CreateApartmentDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        //public int CityCountryId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        //public int MaxGuests { get; set; }
        public ApartmentGuestsDto Guests { get; set; }

        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public int ApartmentTypeId { get; set; }
        public IEnumerable<int> FeatureIds { get; set; }
        public IEnumerable<int> PaymentMethodIds { get; set; }
        public IEnumerable<string> Images { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
    }

    public class ApartmentGuestsDto
    {
        public int Adults { get; set; }
        public int Childrens { get; set; }
        public int TotalRooms { get; set; }
    }
}
