using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup.CityCountry
{
    public class EfUpdateCityCommand : EfUseCase, IUpdateCityCommand
    {
        public EfUpdateCityCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 62;

        public string Name => nameof(EfUpdateCityCommand);

        public void Execute(CityCountryDto data)
        {
            var city = Context.Cities.Include(c => c.CityCountries).FirstOrDefault(x => x.Id == data.CityId);

            if(city == null)
            {
                throw new EntityNotFoundException(nameof(City), (int)data.CityId);
            }

            var cityCountry = city.CityCountries.FirstOrDefault();

            if (cityCountry == null)
            {
                var cityCityCountry = new App.Domain.CityCountry
                {
                    CityId = city.Id,
                    CountryId = (int)data.CountryId
                };

                city.CityCountries.Add(cityCityCountry);
            }

            else if (cityCountry.CountryId != data.CountryId)
            {
                cityCountry.CountryId = (int)data.CountryId;
            }

            city.Name = data.CityName;
            city.IsActive = data.IsActive ?? false;

            Context.SaveChanges();
        }
    }
}
