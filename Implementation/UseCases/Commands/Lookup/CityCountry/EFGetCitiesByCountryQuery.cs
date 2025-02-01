using Application.DTO;
using Application.UseCases.Queries.Lookup;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup.CityCountry
{
    public class EFGetCitiesByCountryQuery : EfUseCase, IGetCitiesByCountryQuery
    {
        public EFGetCitiesByCountryQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 32;

        public string Name => nameof(EFGetCitiesByCountryQuery);

        public IEnumerable<BasicDto> Execute(int countryId)
        {
            var cityCountries = Context.CitiesCountry.Include(x => x.City).ToList();

            IEnumerable<BasicDto> cities = cityCountries.Where(x => x.CountryId == countryId).Select(x => new BasicDto
            {
                Id = x.Id,
                Name = x.City.Name
            }).ToList();

            return cities;

        }
    }
}
