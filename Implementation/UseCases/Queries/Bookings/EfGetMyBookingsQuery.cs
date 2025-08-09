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
    public class EfGetMyBookingsQuery : EfUseCase, IGetMyBookingsQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetMyBookingsQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => nameof(EfGetMyBookingsQuery);

        public PagedResponse<SearchedBookingDto> Execute(BookingSearch search)
        {
            string url = new Uri($"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First()}").AbsoluteUri;

            var query = Context.Bookings.Where(x => x.UserId == _actor.Id).OrderByDescending(x => x.CheckIn)
                                        .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Apartment.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            return query.AsPagedReponse(search, x => new SearchedBookingDto
            {
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                PaymentMethod = x.BookingPayments.Select(b => b.PaymentApartment.Payment.Name).FirstOrDefault().ToString() ?? "/",
                TotalGuests = x.TotalGuests,
                ApartmentId = x.ApartmentId,
                ApartmentName = x.Apartment.Name,
                TotalPrice = x.TotalPrice ?? 0,
                //ApartmentImage = url + x.Apartment.MainImage.Replace("wwwroot\\", ""),
                ApartmentImage = x.Apartment.MainImage,
                Owner = new UserDto
                {
                    FirstName = x.Apartment.User.FirstName,
                    LastName = x.Apartment.User.LastName,
                    Email = x.Apartment.User.Email,
                    Phone = x.Apartment.User.Phone,
                    Avatar = x.Apartment.User.Avatar,
                    Id = x.Id,
                },
                BookingId = x.Id,
                //User = new UserDto
                //{
                //    FirstName = x.User.FirstName,
                //    LastName = x.User.LastName,
                //    Email = x.User.Email,
                //    Phone = x.User.Phone,
                //    Avatar = x.User.Avatar,
                //    Id = x.Id,
                //}
            });
        }
    }
}
