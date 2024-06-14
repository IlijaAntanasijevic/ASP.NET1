using Domain.Core;
using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class PaymentApartment : Entity
    {
        public int ApartmentId { get; set; }
        public int PaymentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Payment Payment { get; set; }

        public ICollection<BookingPayment> BookingPayments { get; set; } = new HashSet<BookingPayment>();


    }
}
