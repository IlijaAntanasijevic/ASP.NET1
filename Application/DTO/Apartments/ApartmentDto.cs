using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class BaseApartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
        public ApartmentImageDto MainImage { get; set; }
        public string ApartmentType { get; set; }
    }
    public class SearchApartmentsDto : BaseApartmentDto
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class ApartmentDto : BaseApartmentDto
    {
        public BasicDto City { get; set; }
        public BasicDto Country { get; set; }
        public string Description { get; set; }
        public int TotalBookings { get; set; }
        public int ApartmentTypeId { get; set; }
        public IEnumerable<BasicDto> PaymentMethods { get; set; }
        //public IEnumerable<int> PaymentMethodIds { get; set; }
        public IEnumerable<BasicDto> Features { get; set; }
        //public IEnumerable<int> FeatureIds { get; set; }
        public IEnumerable<ApartmentImageDto> Images { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public bool UserCanBook { get; set; }

    }

    public class ApartmentImageDto
    {
        public UploadType ImageType { get; set; }
        public string FileName { get; set; }
        public string? OriginalFileName { get; set; }
    }


}
