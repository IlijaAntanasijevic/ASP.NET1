using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class UpdateApartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        //public string MainImage { get; set; }
        public decimal Price { get; set; }
        //public IEnumerable<string> Images { get; set; }
        public IEnumerable<int> FeatureIds { get; set; }
        public IEnumerable<int> PaymentMethodIds { get; set; }
    }

    public class UpdateApartmentImagesDto
    {
        public int Id { get; set; }
        public string MainImage { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
