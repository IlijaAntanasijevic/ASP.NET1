using App.Domain;
using Application;
using Application.DTO.Bookings;
using Application.Exceptions;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using FluentValidation;

using Implementation.Validators;
using Microsoft.EntityFrameworkCore;


namespace Implementation.UseCases.Commands.Bookings
{
    public class EfUpdateBookingCommand : EfUseCase, IUpdateBookingCommand
    {
        private readonly IApplicationActor _actor;
        private readonly UpdateBookingValidator _validator;
        public EfUpdateBookingCommand(BookingContext context, IApplicationActor actor, UpdateBookingValidator validator)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 21;

        public string Name => nameof(EfUpdateBookingCommand);

        public void Execute(EditBookingDto data)
        {
            _validator.ValidateAndThrow(data);

            var booking = Context.Bookings.Include(x => x.BookingPayments).FirstOrDefault(x => x.Id == data.BookingId);

            if (booking == null)
            {
                throw new EntityNotFoundException(nameof(Booking),data.BookingId);
            }

            if(booking.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("The apartment doesn't belongs to the current user and cannot be booked.");
            }

            //exception is handled in the validator??
            var paymentApartment = Context.PaymentApartments.FirstOrDefault(x => x.IsActive && x.PaymentId == data.PaymentId &&
                                                                           x.ApartmentId == booking.ApartmentId);

            
            var bookingPaymets = Context.BookingPayments.Where(x => x.BookingId == data.BookingId && 
                                                               x.PaymentApartmentId == paymentApartment.PaymentId).ToList();

            Context.BookingPayments.RemoveRange(bookingPaymets);

            

            booking.CheckOut = data.CheckOut;
            booking.CheckIn = data.CheckIn;
            booking.TotalGuests = data.TotalGuests;

            booking.BookingPayments = Context.PaymentApartments.Select(x => new BookingPayment
            {
                PaymentApartmentId = paymentApartment.Id,
            }).ToList();

            Context.SaveChanges();
           

        }
    }
}
