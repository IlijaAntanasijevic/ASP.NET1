using App.Domain;
using Application;
using Application.Common;
using Application.DTO.Bookings;
using Application.DTO.Users;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfCreateBookingCommand : EfUseCase, ICreateBookingCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateBookingValidator _validator;
        private readonly IEmailSender _emailSender;
        public EfCreateBookingCommand(BookingContext context, IApplicationActor actor, CreateBookingValidator validator, IEmailSender emailSender)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 20;

        public string Name => nameof(EfCreateBookingCommand);

        public void Execute(BookingDto data)
        {
            ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task ExecuteInternal(BookingDto data)
        {
            _validator.ValidateAndThrow(data);

            bool apartmentBelongsToUser = Context.Apartments.Any(x => x.UserId == _actor.Id && x.Id == data.ApartmentId);

            if (apartmentBelongsToUser)
            {
                throw new PermissionDeniedException("The apartment belongs to the current user and cannot be booked.");
            }

            //var userAlreadyHaveBooking = Context.Bookings.Any(b => b.UserId == _actor.Id && b.IsActive &&
            //           (
            //               (data.CheckIn >= b.CheckIn && data.CheckIn < b.CheckOut) ||
            //               (data.CheckOut > b.CheckIn && data.CheckOut <= b.CheckOut) ||
            //               (data.CheckIn < b.CheckIn && data.CheckOut > b.CheckOut)
            //           ));

            var userAlreadyHaveBooking = Context.Bookings.Any(b =>  b.UserId == _actor.Id &&
                                    b.IsActive && 
                                    data.CheckIn <= b.CheckOut && 
                                    b.CheckIn <= data.CheckOut);

            if (userAlreadyHaveBooking)
            {
                throw new Application.Exceptions.ValidationException("You already have a reservation that overlaps with the selected dates.");
            }


           var paymentApartment = Context.PaymentApartments.FirstOrDefault(x => x.IsActive && x.PaymentId == data.PaymentId &&
                                                                            x.ApartmentId == data.ApartmentId);


            var booking = new Booking
            {
                CheckIn = data.CheckIn,
                CheckOut = data.CheckOut,
                ApartmentId = data.ApartmentId,
                TotalGuests = data.TotalGuests,
                UserId = _actor.Id,
                IsActive = true,
                BookingPayments = Context.PaymentApartments.Select(x => new BookingPayment
                {
                    PaymentApartmentId = paymentApartment.Id,
                }).ToList(),
            };

            Context.Bookings.Add(booking);
            Context.SaveChanges();
            Context.Entry(booking).Reload();
            Context.Entry(booking).Reference(b => b.Apartment).Load();
            Context.Entry(booking).Reference(b => b.User).Load();
            Context.Entry(booking.Apartment).Reference(a => a.User).Load();

            var emailData = new ConfirmedBookingEmailDto
            {
                CheckIn = booking.CheckIn.ToString("yyyy-MM-dd"),
                CheckOut = booking.CheckOut.ToString("yyyy-MM-dd"),
                Adults = booking.TotalGuests.ToString(),
                Childrens = booking.TotalGuests.ToString(),
                PricePerNight = booking.Apartment.Price.ToString(),
                TotalPrice = booking.TotalPrice.ToString(),
                Address = booking.Apartment.Address,
                OwnerName = booking.Apartment.User.FirstName,
                OwnerLastName = booking.Apartment.User.LastName,
                OwnerPhone = booking.Apartment.User.Phone,
                OwnerEmail = booking.Apartment.User.Email,
                UserName = booking.User.FirstName,
                UserLastName = booking.User.LastName,
                UserPhone = booking.User.Phone,
                Email = booking.User.Email
            };

            await _emailSender.BookingConfirmed(emailData);
            await _emailSender.NewReservationAsync(new NewReservationDto
            {
                CheckIn = booking.CheckIn.ToString("yyyy-MM-dd"),
                CheckOut = booking.CheckOut.ToString("yyyy-MM-dd"),
                Adults = booking.TotalGuests.ToString(),
                Childrens = booking.TotalGuests.ToString(),
                TotalPrice = booking.TotalPrice.ToString(),
                Address = booking.Apartment.Address,
                UserName = booking.User.FirstName,
                UserLastName = booking.User.LastName,
                UserPhone = booking.User.Phone,
                UserEmail = booking.User.Email,
                EmailToSend = booking.Apartment.User.Email
            });
        }
    }
}

