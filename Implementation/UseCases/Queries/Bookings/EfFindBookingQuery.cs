using Application;
using Application.DTO;
using Application.DTO.Search;
using Application.Exceptions;
using Application.UseCases.Queries.Bookings;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using App.Domain;
using Application.DTO.Users;
using Application.DTO.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries.Bookings
{
    public class EfFindBookingQuery : EfUseCase, IFindBookingQuery
    {
        private readonly IApplicationActor _actor;
        public EfFindBookingQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;

        public string Name => nameof(EfFindBookingQuery);

        public FindBookingDto Execute(int search)
        {
            var booking = Context.Bookings.Include(x => x.Apartment)
                                          .ThenInclude(x => x.CityCountry)
                                          .ThenInclude(x => x.City)
                                          .Include(x => x.Apartment)
                                          .ThenInclude(x => x.CityCountry)
                                          .ThenInclude(x => x.Country)
                                          .Include(x => x.User)
                                          .Include(x => x.BookingPayments)
                                          .ThenInclude(x => x.PaymentApartment)
                                          .ThenInclude(x => x.Payment)
                                          .Include(x => x.Apartment.ApartmentType)
                                          .FirstOrDefault(x => x.Id == search && x.IsActive);

            if (booking == null)
            {
                throw new EntityNotFoundException(nameof(Booking), search);
            }

            if (booking.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("You don't have permission!");
            }

 
            return new FindBookingDto
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                PaymentMethod = booking.BookingPayments.Select(b => b.PaymentApartment.Payment.Name).FirstOrDefault().ToString(),
                TotalGuests = booking.TotalGuests,
                User = new UserDto
                {
                    FirstName = booking.User.FirstName,
                    LastName = booking.User.LastName,
                    Email = booking.User.Email,
                    Phone = booking.User.Phone,
                    Avatar = booking.User.Avatar,
                    Id = booking.Id,
                },
                ApartmentId = booking.ApartmentId,
                BookingId = booking.Id,
                City = booking.Apartment.CityCountry.City.Name,
                Country = booking.Apartment.CityCountry.Country.Name,
                ApartmentType = booking.Apartment.ApartmentType.Name

            };
        }
    }
}
