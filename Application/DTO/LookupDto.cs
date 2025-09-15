using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LookupDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PaymentMethodsDto : LookupDto
    {
        public decimal ProcessingFee { get; set; }
    }

    public class CityDto : LookupDto
    {
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string Currency { get; set; }
        public int TotalApartments { get; set; }
        public decimal AvgPrice { get; set; }
    }
}
