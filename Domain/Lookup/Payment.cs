using System;
using System.Collections.Generic;
using System.Text;
using App.Domain;


namespace Domain.Lookup
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? ProcessingFee { get; set; }
        public bool IsActive { get; set; } = true;
        public string Icon { get; set; }

        public virtual ICollection<PaymentApartment> PaymentApartments { get; set; } = new HashSet<PaymentApartment>();
    }
}
