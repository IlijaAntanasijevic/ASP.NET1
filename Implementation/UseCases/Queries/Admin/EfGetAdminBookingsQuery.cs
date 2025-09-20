using App.Domain;
using Application.DTO.Admin;
using Application.UseCases.Queries.Admin;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminBookingsQuery : EfUseCase, IGetAdminBookingsQuery
    {
        public EfGetAdminBookingsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 59;

        public string Name => nameof(EfGetAdminBookingsQuery);

        public List<AdminBookingsDto> Execute(AdminBookingsSearchDto search)
        {
            var bookings = Context.Bookings.Include(x => x.User)
                .Include(x => x.Apartment)
                .ThenInclude(x => x.User)
                .Include(x => x.Apartment)
                .ThenInclude(x => x.CityCountry)
                .ThenInclude(x => x.City).AsQueryable();

            if(search.OwnerId.HasValue && search.OwnerId.Value > 0)
            {
                bookings = bookings.Where(x => x.Apartment.UserId == search.OwnerId.Value);
            }

            if(search.GuestId.HasValue && search.GuestId.Value > 0)
            {
                bookings = bookings.Where(X => X.UserId == search.GuestId.Value);
            }

            if(search.CityId.HasValue && search.CityId.Value > 0)
            {
                bookings = bookings.Where(x => x.Apartment.CityCountry.CityId  == search.CityId.Value);
            }

            if (search.Status.HasValue && search.Status.Value > 0)
            {
                switch ((BookingStatus)search.Status.Value)
                {
                    case BookingStatus.Upcoming:
                        bookings = bookings.Where(x => x.IsActive && x.CheckOut >= DateTime.Today);
                        break;

                    case BookingStatus.Completed:
                        bookings = bookings.Where(x =>  x.IsActive && x.CheckOut < DateTime.Today);
                        break;

                    case BookingStatus.Canceled:
                        bookings = bookings.Where(x => !x.IsActive);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                string keyword = search.Keyword.ToLower().Trim();
                bookings = bookings.Where(x => x.Apartment.Name.ToLower().Contains(keyword));
            }

            var response = bookings.Select(x => new AdminBookingsDto
            {
                Id = x.Id,
                ApartmentId = x.ApartmentId,
                ApartmentImage = x.Apartment.MainImage,
                ApartmentName = x.Apartment.Name,
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut,
                City = x.Apartment.CityCountry.City.Name,
                GuestFullName = x.User.FirstName + " " + x.User.LastName,
                GuestId = x.User.Id,
                OwnerFullName = x.Apartment.User.FirstName + " " + x.Apartment.User.LastName,
                OwnerId = x.Apartment.UserId,
                Status = (int)(!x.IsActive ? BookingStatus.Canceled : (x.CheckOut < DateTime.Today ? BookingStatus.Completed : BookingStatus.Upcoming)),
                TotalGuests = x.TotalGuests,
                TotalPrice = (decimal)x.TotalPrice
            }).ToList();

            return response;
        }
    }
}
