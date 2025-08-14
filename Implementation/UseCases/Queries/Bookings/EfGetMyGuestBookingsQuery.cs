using Application;
using Application.DTO;
using Application.DTO.Search;
using Application.DTO.Users;
using Application.UseCases.Queries.Bookings;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Bookings
{
    public class EfGetMyGuestBookingsQuery : EfUseCase, IGetMyGuestBookingsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetMyGuestBookingsQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 37;

        public string Name => nameof(EfGetMyGuestBookingsQuery);

        public List<SearchedBookingDto> Execute(BookingSearch search)
        {
            string url = new Uri($"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First()}").AbsoluteUri;

            var query = Context.Bookings
                .Where(x => x.Apartment.UserId == _actor.Id && x.IsActive)
                .Include(x => x.Apartment.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Apartment.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            return query.Select(x => new SearchedBookingDto
            {
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                PaymentMethod = x.BookingPayments.Select(b => b.PaymentApartment.Payment.Name).FirstOrDefault(),
                TotalGuests = x.TotalGuests,
                ApartmentId = x.ApartmentId,
                ApartmentName = x.Apartment.Name,
                TotalPrice = (decimal)x.TotalPrice,
                ApartmentImage = x.Apartment.MainImage,
                BookingId = x.Id,
                User = new UserDto
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    Avatar = x.User.Avatar,
                }
            }).ToList();
        }
    }
}
