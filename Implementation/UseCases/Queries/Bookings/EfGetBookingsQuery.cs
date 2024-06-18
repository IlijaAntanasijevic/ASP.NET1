using Application;
using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.DTO.Users;
using Application.UseCases.Queries.Bookings;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries.Bookings
{
    public class EfGetBookingsQuery : EfUseCase, IGetBookingsQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetBookingsQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => nameof(EfGetBookingsQuery);

        public PagedResponse<SearchedBookingDto> Execute(BookingSearch search)
        {
            var query = Context.Bookings.Where(x => x.UserId == _actor.Id)
                                        .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Apartment.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            return query.AsPagedReponse(search, x => new SearchedBookingDto
            {
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                PaymentMethod = x.BookingPayments.Select(b => b.PaymentApartment.Payment.Name).FirstOrDefault().ToString(),
                TotalGuests = x.TotalGuests,
                ApartmentId = x.ApartmentId,
                BookingId = x.Id,
                User = new UserDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    Avatar = x.User.Avatar,
                    Id = x.Id,
                }
            });
        }
    }
}
