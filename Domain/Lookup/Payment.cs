using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Lookup
{
    public class Payment
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public virtual ICollection<PaymentApartment> PaymentApartments { get; set; } = new HashSet<PaymentApartment>();
    }
}
