using App.Domain;
using Application.DTO.Admin;
using Application.DTO.Search;
using Application.UseCases.Queries.Admin;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminDashboardQuery : EfUseCase, IGetAdminDashboardQuery
    {
        public EfGetAdminDashboardQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 54;

        public string Name => nameof(EfGetAdminDashboardQuery);

        public AdminDashboardDto Execute(BasicSearch search)
        {
            int totalUsers = Context.Users.Count(x => x.IsActive == true);
            int totalApartments = Context.Apartments.Count(x => x.IsActive == true && x.IsArchived == false);
            int totalBookings = Context.Bookings.Count(x => x.IsActive == true);
            int totalDeletedApartments = Context.Apartments.Count(x => x.IsActive == false);

            var newBookings = Context.Bookings.Include(x => x.Apartment)
                .ThenInclude(x => x.User)
                .Include(x => x.Apartment)
                .ThenInclude(x => x.CityCountry)
                .ThenInclude(x => x.City)
                .Include(x => x.Apartment)
                .ThenInclude(x => x.CityCountry)
                .ThenInclude(x => x.Country)
                .OrderByDescending(x => x.CreatedAt)
                .Take(10);

            var newApartments = Context.Apartments.Include(x => x.CityCountry)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.CityCountries)
                .ThenInclude(x => x.Country)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .Take(10);

            var response = new AdminDashboardDto
            {
                NewApartments = newApartments.Select(x => new NewApartmentsDto
                {
                    ApartmentName = x.Name,
                    CreatedAt = x.CreatedAt,
                    Location = x.CityCountry.City.Name + ", " + x.CityCountry.Country.Name,
                    OwnerName = x.User.FirstName,
                    Status = (int)(x.IsArchived == true ? ApartmentStatus.Archived : x.IsActive ? ApartmentStatus.Active : ApartmentStatus.Deleted),
                    TotalRoomns = x.TotalRooms,
                    Price = x.Price
                }).ToList(),

                NewBookings = newBookings.Select(x => new NewBookingsDto
                {
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    Location = x.Apartment.CityCountry.City.Name + ", " + x.Apartment.CityCountry.Country.Name,
                    OwnerName = x.Apartment.User.FirstName,
                    TotalGuests = x.TotalGuests,
                    TotalPrice = (decimal)x.TotalPrice,
                    Status = (int)(!x.IsActive ? BookingStatus.Canceled : (x.CheckOut < DateTime.Today ? BookingStatus.Completed : BookingStatus.Upcoming)),
                    TotalNights = (x.CheckOut - x.CheckIn).Days
                }).ToList(),

                Statistics = new StatisticsDto
                {
                    DeletedApartments = totalDeletedApartments,
                    TotalApartments = totalApartments,
                    TotalBookings = totalBookings,
                    TotalUsers = totalUsers,
                }
            };

            return response;
        }
    }
}
