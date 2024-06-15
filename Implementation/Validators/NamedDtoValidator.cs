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
    public abstract class NamedDtoValidator<TDto> : AbstractValidator<NamedDto>
        where TDto : NamedDto
    {
        public NamedDtoValidator()
        {
            var name = this.GetType().Name;
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage($"{name} name is required")
                                .MinimumLength(2)
                                .WithMessage("Min number of characters is 2.");
                               
        }

    }

    /*
    public class CreateCityValidator : NamedDtoValidator<NamedDto>
    {
        public CreateCityValidator(BookingContext context)
        {
            RuleFor(x => x.Name).Must(name => !context.Cities.Any(c => c.Name == name))
                                .WithMessage("City name must be unique");
        }
    }
    */

}
