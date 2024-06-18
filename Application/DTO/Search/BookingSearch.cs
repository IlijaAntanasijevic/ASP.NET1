using App.Domain;
using Application.DTO.Bookings;
using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class BookingSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }

    public class SearchedBookingDto : BasicBookingDto
    {
        public int BookingId { get; set; }
        public int ApartmentId { get; set; }
        public UserDto User { get; set; }
        public string PaymentMethod { get; set; }

    }
}
