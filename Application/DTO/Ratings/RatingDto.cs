using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Ratings
{
    public class RatingDto
    {
    }

    public class CreateRatingDto
    {
        public int ApartmentId { get; set; }

        public string Message { get; set; }
        public List<RaingValuesDto> Values { get; set; }
    }

    public class RaingValuesDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
    }
}
