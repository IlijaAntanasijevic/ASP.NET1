using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminDashboardDto
    {
        public StatisticsDto Statistics { get; set; }
        public List<NewBookingsDto> NewBookings { get; set; }
        public List<NewApartmentsDto> NewApartments { get; set; }
    }

    public class StatisticsDto
    {
        public int TotalUsers { get; set; }
        public int TotalApartments { get; set; }
        public int TotalBookings { get; set; }
        public int DeletedApartments { get; set; }
    }

    public class NewBookingsDto
    {
        public string Location { get; set; }
        public string OwnerName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Status { get; set; } //confirmed, pending,canceled
        public int TotalNights { get; set; }
        public int TotalGuests { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class NewApartmentsDto
    {
        public string ApartmentName { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; } //confirmed, active, deleted, pending
        public string Location { get; set; }
        public int TotalRoomns { get; set; }
        public decimal Price { get; set; }
    }
}
