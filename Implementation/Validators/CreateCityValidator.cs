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


    public class CreateCountryValidator : AbstractValidator<NamedDto>
    {
        public CreateCountryValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage($"Country name is required")
                              .MinimumLength(2)
                              .WithMessage("Min number of characters is 2.")
                              .Must(name => !context.Countries.Any(c => c.Name == name))
                              .WithMessage("Country name must be unique");
        }
    }

    public class CreateFeaturesValidator : AbstractValidator<NamedDto>
    {
        public CreateFeaturesValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage($"Feature name is required")
                              .MinimumLength(2)
                              .WithMessage("Min number of characters is 2.")
                              .Must(name => !context.Features.Any(c => c.Name == name))
                              .WithMessage("Feature name must be unique");
        }
    }

    public class CreatePaymentValidator : AbstractValidator<NamedDto>
    {
        public CreatePaymentValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                              .WithMessage($"Payment name is required")
                              .MinimumLength(2)
                              .WithMessage("Min number of characters is 2.")
                              .Must(name => !context.Payments.Any(c => c.Name == name))
                              .WithMessage("Payment name must be unique");
        }
    }

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
