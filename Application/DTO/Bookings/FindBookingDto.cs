using Application.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Bookings
{
    public class FindBookingDto : SearchedBookingDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string ApartmentType { get; set; }
    }
}
