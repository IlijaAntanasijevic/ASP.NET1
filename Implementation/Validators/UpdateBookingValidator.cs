using App.Domain;
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
    public class UpdateBookingValidator : AbstractValidator<EditBookingDto>
    {
        private readonly BookingContext _context;
        //private readonly Booking currentBooking;
        public UpdateBookingValidator(BookingContext context)
        {
           
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.BookingId).NotEmpty()
                                     .WithMessage("Booking id is required")
                                     .Must(id => context.Bookings.Any(x => x.Id == id))
                                     .WithMessage("Booking with provided id doesn't exist.");
                                

            RuleFor(x => x.CheckIn)
                    .NotEmpty()
                    .WithMessage("Check-in date is required.")
                    .Must(BeAFutureDate)
                    .WithMessage("Check-in date must be in the future.");

            RuleFor(x => x.CheckOut)
                .NotEmpty()
                .WithMessage("Check-out date is required.")
                .Must(BeAFutureDate)
                .WithMessage("Check-out date must be in the future.")
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be later than check-in date.");


            RuleFor(x => x.TotalGuests).GreaterThan(0)
                                       .WithMessage("Minimum number of guests is 1.")
                                       .Must(CheckTotalGuests)
                                       .WithMessage("The apartment does not accept that number of guests.");
                                      



            RuleFor(x => x.PaymentId).NotEmpty()
                                     .WithMessage("Payment id is required")
                                     .Must(PaymentMethodIsValid)
                                     .WithMessage("The specified payment method for this apartment does not exist or is not active.");


            RuleFor(dto => dto).Must(IsApartmentAvailable)
                               .WithMessage("The apartment is not available for the selected dates.")
                               .Must(BookingCanBeEdited)
                               .WithMessage("Booking cannot be edited within one day of check-in.");
        }

        private bool BookingCanBeEdited(EditBookingDto booking)
        {
            return booking.CheckIn > DateTime.Now.AddDays(1);
        }

        private bool BeAFutureDate(DateTime date)
        {
            return date > DateTime.Now;
        }

        private bool IsApartmentAvailable(EditBookingDto data)
        {
            var currentBooking = _context.Bookings.FirstOrDefault(x => x.Id == data.BookingId);
            if (currentBooking == null)
            {
                return false;
            }
            var apartmentId = currentBooking.ApartmentId;

            bool isAvailable = !_context.Bookings.Any(x => x.ApartmentId == apartmentId &&
                                                      x.Id != data.BookingId &&
                                                     ((data.CheckIn >= x.CheckIn && data.CheckIn < x.CheckOut) ||
                                                     (data.CheckOut > x.CheckIn && data.CheckOut <= x.CheckOut) ||
                                                     (data.CheckIn < x.CheckIn && data.CheckOut > x.CheckOut)));

            return isAvailable;
        }

        private bool PaymentMethodIsValid(EditBookingDto dto, int paymentId)
        {
            var currentBooking = _context.Bookings.FirstOrDefault(x => x.Id == dto.BookingId);
            if (currentBooking == null)
            {
                return false;
            }
                

            return _context.PaymentApartments.Any(x => x.PaymentId == paymentId && x.IsActive && x.ApartmentId == currentBooking.ApartmentId);
        }

        private bool CheckTotalGuests(EditBookingDto dto, int number)
        {
  
            var currentBooking = _context.Bookings.FirstOrDefault(x => x.Id == dto.BookingId);
            if (currentBooking == null)
            {
                return false;
            }

            var apartment = _context.Apartments.FirstOrDefault(x => x.Id == currentBooking.ApartmentId);

            if (apartment == null)
            {
                return false;
            }

            return apartment.MaxGuests >= number;
        }
    }
}
