using App.Domain;
using Application;
using Application.DTO.Bookings;
using Application.Exceptions;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfCreateBookingCommand : EfUseCase, ICreateBookingCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateBookingValidator _validator;
        public EfCreateBookingCommand(BookingContext context, IApplicationActor actor, CreateBookingValidator validator)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => nameof(EfCreateBookingCommand);

        public void Execute(BookingDto data)
        {
            _validator.ValidateAndThrow(data);

            bool apartmentBelongsToUser = Context.Apartments.Any(x => x.UserId == _actor.Id && x.Id == data.ApartmentId);

            if (apartmentBelongsToUser)
            {
                throw new PermissionDeniedException("The apartment belongs to the current user and cannot be booked.");
            }

            var paymentApartment = Context.PaymentApartments.FirstOrDefault(x => x.IsActive && x.PaymentId == data.PaymentId && x.ApartmentId == data.ApartmentId);

            if (paymentApartment == null)
            {
                throw new ValidationException("The specified payment method for this apartment does not exist or is not active.");
            }

            var booking = new Booking
            {
                CheckIn = data.CheckIn,
                CheckOut = data.CheckOut,
                ApartmentId = data.ApartmentId,
                TotalGuests = data.TotalGuests,
                UserId = _actor.Id,
                BookingPayments = Context.PaymentApartments.Select(x => new BookingPayment
                {
                    PaymentApartmentId = paymentApartment.Id,
                }).ToList(),

            };

            Context.Bookings.Add(booking);
            Context.SaveChanges();

        }
    }
}
