using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class CreateApartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityCountryId { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
        public string Phone { get; set; }
        public string MainImage { get; set; }
        //public int UserId { get; set; }
        public int ApartmentTypeId { get; set; }
        public List<int> FeatureIds { get; set; }
        public List<int> PaymentMethodIds { get; set; }
        public IEnumerable<string> Images { get; set; }


    }
}
