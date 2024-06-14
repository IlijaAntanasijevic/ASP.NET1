using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class BookingPayment
    {
        public int PaymentApartmentId { get; set; }
        public int BookingId { get; set; }

        public virtual PaymentApartment PaymentApartment { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
