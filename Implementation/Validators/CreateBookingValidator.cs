using Application.DTO.Bookings;
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
    public class CreateBookingValidator : AbstractValidator<BookingDto>
    {
        private readonly BookingContext _context;
        public CreateBookingValidator(BookingContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CheckIn)
                    .NotEmpty()
                    .WithMessage("Check-in date is required.")
                    .Must(BeAValidDate)
                    .WithMessage("Check-in date must be a valid date.")
                    .Must(BeAFutureDate)
                    .WithMessage("Check-in date must be in the future.");

            RuleFor(x => x.CheckOut)
                .NotEmpty()
                .WithMessage("Check-out date is required.")
                .Must(BeAValidDate)
                .WithMessage("Check-out date must be a valid date.")
                .Must(BeAFutureDate)
                .WithMessage("Check-out date must be in the future.")
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be later than check-in date.");

            RuleFor(x => x.TotalGuests)
                .GreaterThan(0)
                .WithMessage("Total guests must be greater than zero.")
                .LessThanOrEqualTo(100)
                .WithMessage("Total guests must not exceed 100.");

            RuleFor(x => x.ApartmentId).NotEmpty()
                                       .WithMessage("Apartment id is required")
                                       .Must(id => context.Apartments.Any(x => x.Id == id && x.IsActive))
                                       .WithMessage("Apartment doesn't exist");

            RuleFor(x => x.PaymentId).NotEmpty()
                                     .WithMessage("Payment id is required")
                                     .Must((dto, id) => context.PaymentApartments.Any(x => x.PaymentId == id && x.IsActive &&
                                                                                      x.ApartmentId == dto.ApartmentId))
                                     .WithMessage("The specified payment method for this apartment does not exist or is not active.");


            RuleFor(dto => dto).Must(IsApartmentAvailable)
                               .WithMessage("The apartment is not available for the selected dates.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeAFutureDate(DateTime date)
        {
            return date > DateTime.Now;
        }

        private bool IsApartmentAvailable(BookingDto data)
        {
            bool isAvailable = !_context.Bookings.Any(x =>
                (data.CheckIn >= x.CheckIn && data.CheckIn < x.CheckOut) ||
                (data.CheckOut > x.CheckIn && data.CheckOut <= x.CheckOut) ||
                (data.CheckIn < x.CheckIn && data.CheckOut > x.CheckOut));

            return isAvailable;
        }
    }
    
}
