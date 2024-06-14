using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class Booking : Entity
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Decimal TotalPrice { get; set; }
        public int TotalGuests { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }

        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
        public ICollection<BookingPayment> BookingPayments { get; set; } = new HashSet<BookingPayment>();

    }
}
