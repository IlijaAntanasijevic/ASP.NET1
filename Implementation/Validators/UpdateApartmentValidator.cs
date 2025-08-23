using Application.DTO.Apartments;
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
    public class UpdateApartmentValidator : AbstractValidator<UpdateApartmentDto>
    {
        public UpdateApartmentValidator(BookingContext context)
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

            RuleFor(x => x.Guests.Adults).NotEmpty()
                                     .WithMessage("Number of adults is required.")
                                     .LessThan(16)
                                     .WithMessage("Maximun number of adults is 15.")
                                     .GreaterThan(0)
                                     .WithMessage("Minimun number of adults is 1.");

            RuleFor(x => x.Guests.Childrens).NotEmpty()
                                  .WithMessage("Number of childrens is required.")
                                  .LessThan(16)
                                  .WithMessage("Maximun number of childrens is 15.")
                                  .GreaterThan(0)
                                  .WithMessage("Minimun number of childrens is 1.");

            RuleFor(x => x.Guests.TotalRooms).NotEmpty()
                                  .WithMessage("Number of total rooms is required.")
                                  .LessThan(16)
                                  .WithMessage("Maximun number of total rooms is 15.")
                                  .GreaterThan(0)
                                  .WithMessage("Minimun number of total rooms is 1.");

            RuleFor(x => x.Price).NotEmpty()
                                 .WithMessage("Price is required.")
                                 .GreaterThan(0)
                                 .WithMessage("Minimum price per night must be greather than 0.")
                                 .LessThan(10000)
                                 .WithMessage("Maximum price per night must be less than 10.000.");

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
        }
    }
}
