using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class CheckApartmentDto
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int ApartmentId { get; set; }

    }
}
