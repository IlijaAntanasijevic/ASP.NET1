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
            RuleFor(x => x.CityId).NotEmpty()
                                  .WithMessage("City id is required")
                                  .Must(cityId => !context.CitiesCountry.Any(c => c.CityId == cityId))
                                  .WithMessage("City already exist");

            RuleFor(x => x.CountryId).NotEmpty()
                                     .WithMessage("Country id is required");
            
        }
    }
}
