using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class ApartmentSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public List<SortBy> Sorts { get; set; } = new List<SortBy>();
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ApartmentTypeId { get; set; }
    }
}
