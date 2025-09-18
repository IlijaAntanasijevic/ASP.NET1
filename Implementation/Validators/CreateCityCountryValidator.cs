using App.Domain;
using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateCityCountryValidator : AbstractValidator<CityCountryDto>
    {
        public CreateCityCountryValidator(BookingContext context)
        {
            RuleFor(x => x.CityName).NotEmpty()
                                  .WithMessage("City is required")
                                  .Must(city => !context.CitiesCountry.Any(c => c.City.Name == city))
                                  .WithMessage("City already exist");

            RuleFor(x => x.CountryId).NotEmpty()
                                     .WithMessage("Country id is required");
            
        }
    }
}
