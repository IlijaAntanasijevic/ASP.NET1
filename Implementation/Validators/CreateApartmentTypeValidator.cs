using Application.DTO;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateApartmentTypeValidator : AbstractValidator<NamedDto>
    {

        public CreateApartmentTypeValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull()
                                .WithMessage("Apartment type is required.")
                                .MinimumLength(3)
                                .WithMessage("Min number of characters is 3.")
                                .Must(name => !context.ApartmentTypes.Any(x => x.Name == name))
                                .WithMessage("Apartment type is in use.");
        }
    }
}
