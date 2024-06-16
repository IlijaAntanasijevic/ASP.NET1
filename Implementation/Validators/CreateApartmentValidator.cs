using App.Domain;
using Application.DTO.Apartments;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
  
    public class CreateApartmentValidator : AbstractValidator<CreateApartmentDto>
    {
        public CreateApartmentValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Apartment name is required")
                                .MinimumLength(5)
                                .WithMessage("Apartment name must be at least 5 characters long.");

            RuleFor(x => x.Description).NotEmpty()
                                       .WithMessage("Apartment description is required.")
                                       .MinimumLength(10)
                                       .WithMessage("Description must be at least 10 characters long.");

            RuleFor(x => x.Address).NotEmpty()
                                   .WithMessage("Address is required.")
                                   .MinimumLength(5)
                                   .WithMessage("Address must be at least 5 characters long.");

            RuleFor(x => x.CityCountryId).NotEmpty()
                                         .WithMessage("CityCountry id is required.")
                                         .Must(id => context.CitiesCountry.Any(c => c.Id == id))
                                         .WithMessage("City-Country id doesn't exist.");

            RuleFor(x => x.MaxGuests).NotEmpty()
                                     .WithMessage("Number of max guests is required.")
                                     .LessThan(16)
                                     .WithMessage("Maximun number of guests is 15.")
                                     .GreaterThan(0)
                                     .WithMessage("Minimun number of guests is 1.");

            RuleFor(x => x.Price).NotEmpty()
                                 .WithMessage("Price is required.")
                                 .GreaterThan(0)
                                 .WithMessage("Minimum price per night must be greather than 0.")
                                 .LessThan(10000)
                                 .WithMessage("Maximum price per night must be less than 10.000.");

            RuleFor(x => x.ApartmentTypeId).NotEmpty()
                                           .WithMessage("Apartment type id is required.")
                                           .Must(id => context.ApartmentTypes.Any(x => x.Id == id))
                                           .WithMessage("Apartment type id doesn't exist.");

            RuleFor(x => x.FeatureIds).NotEmpty()
                                      .WithMessage("At least one feature is required.")
                                      .DependentRules(() =>
                                      {
                                          RuleForEach(x => x.FeatureIds).Must(id => context.Features.Any(f => f.Id == id && f.IsActive))
                                                                        .WithMessage("Feature doesn't exist.");
                                      });

            RuleFor(x => x.PaymentMethodIds).NotEmpty()
                                            .WithMessage("At least one payment method is required.")
                                            .DependentRules(() =>
                                            {
                                                RuleForEach(x => x.PaymentMethodIds).Must(id => context.Payments.Any(p => p.Id == id))
                                                                                    .WithMessage("Payment method doesn't exist.");
 
                                            });

           // RuleFor(x => x.MainImage).NotEmpty();



        }
    }
}
