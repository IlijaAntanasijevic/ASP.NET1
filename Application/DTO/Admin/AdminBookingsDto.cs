using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminBookingsDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentImage { get; set; } 
        public string City { get; set; }
        public int OwnerId { get; set; }
        public string OwnerFullName { get; set; }
        public int GuestId { get; set; }
        public string GuestFullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int TotalGuests { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AdminBookingsSearchDto
    {
        public int? OwnerId { get; set; }
        public int? GuestId { get; set; }
        public int? Status { get; set; }
        public int? CityId { get; set; }
        public string Keyword { get; set; }
            
    }
}
