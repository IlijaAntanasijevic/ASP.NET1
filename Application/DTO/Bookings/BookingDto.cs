using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Bookings
{
    public abstract class BasicBookingDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int TotalGuests { get; set; }
        
    }

    public class BookingDto : BasicBookingDto
    {
        public int ApartmentId { get; set; }
        public int PaymentId { get; set; }

    }

    public class EditBookingDto : BasicBookingDto
    {
        public int BookingId { get; set; }
        public int PaymentId { get; set; }

    }
}
