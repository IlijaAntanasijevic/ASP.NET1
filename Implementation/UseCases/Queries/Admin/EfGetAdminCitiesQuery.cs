using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Lookup;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminCitiesQuery : EfUseCase, IGetAdminCitiesQuery
    {
        public EfGetAdminCitiesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 60;

        public string Name => nameof(EfGetAdminCitiesQuery);

        public List<CityDto> Execute(BasicSearch search)
        {
            var cities = Context.Cities.Include(x => x.CityCountries).ThenInclude(x => x.Country)
                .Include(x => x.CityCountries).ThenInclude(x => x.Apartments);

            return cities.Select(x => new CityDto
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.CityCountries.Select(c => c.Country.Name).FirstOrDefault(),
                CountryId = x.CityCountries.Select(c => c.Country.Id).FirstOrDefault(),
                Icon = null,
                AvgPrice = x.CityCountries.SelectMany(c => c.Apartments).Any() ? x.CityCountries.SelectMany(x => x.Apartments).Average(a => a.Price) : 0,
                Currency = x.CityCountries.Select(c => c.Country.Currency).FirstOrDefault(),
                IsActive = x.IsActive,
                TotalApartments = x.CityCountries.SelectMany(c => c.Apartments).Count(a => a.IsActive)
            }).ToList();
        }
    }
}
