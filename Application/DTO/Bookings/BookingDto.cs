using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Bookings
{
    public class BookingDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int TotalGuests { get; set; }
        public int ApartmentId { get; set; }
        public int PaymentId { get; set; }
    }
}
