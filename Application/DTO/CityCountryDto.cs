using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CityCountryDto
    {
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public bool? IsActive { get; set; }
        [JsonPropertyName("name")]
        public string CityName { get; set; }
    }
}
