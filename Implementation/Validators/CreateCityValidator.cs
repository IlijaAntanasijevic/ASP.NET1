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
    public class CreateCityValidator : AbstractValidator<NamedDto>
    {
        public CreateCityValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage($"City name is required")
                              .MinimumLength(2)
                              .WithMessage("Min number of characters is 2.")
                              .Must(name => !context.Cities.Any(c => c.Name == name))
                              .WithMessage("City name must be unique");
        }
    }
}
