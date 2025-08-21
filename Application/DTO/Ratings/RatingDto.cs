using Application.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Ratings
{
    public class RatingSearchDto : PagedSearch
    {
        public int Id { get; set; }
    }
    public class RatingDto
    {
        public int TotalRatings { get; set; }
        public float AvgRating { get; set; }
        public List<RatingValuesDto> RatingStatistic { get; set; }
    }

    public class RatingDetailDto
    {
        public DateTime Date { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public float AvgRating { get; set; }
        public string Avatar { get; set; }
    }

    public class CreateRatingDto
    {
        public int ApartmentId { get; set; }
        public string Comment { get; set; }
        public List<RatingValuesDto> Values { get; set; }
    }

    public class RatingValuesDto
    {
        public int Id { get; set; }
        public float Value { get; set; }
    }
}
