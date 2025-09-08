using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminApartmentsSearchDto
    {
        public int? UserId { get; set; }
        public int? TotalBookings { get; set; }
        public int? CityId { get; set; }
        public int? Status { get; set; }
    }
    public class AdminApartmentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string OwnerFullName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int TotalBookings { get; set; }
        public int Status { get; set; }
    }
}
