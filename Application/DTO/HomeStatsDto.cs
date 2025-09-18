using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class HomeStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalApartments { get; set; }
        public int TotalBookings { get; set; } 
        public double AvgRating { get; set; }
        public int TotalReviews { get; set; }
    }

    public class HomeTestimonials
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public string UserFullName { get; set; }
        public string Avatar { get; set; }
        public double Rating { get; set; }
        public string RatingInfo { get; set; }
        public string Location { get; set; }
        public string Created { get; set; }
    }
}
