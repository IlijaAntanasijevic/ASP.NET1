using App.Domain;
using Application.DTO.Admin;
using Application.UseCases.Queries.Admin;
using DataAccess;
using Implementation.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminApartmentsQuery : EfUseCase, IGetAdminApartmentsQuery
    {
        public EfGetAdminApartmentsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 55;

        public string Name => nameof(EfGetAdminApartmentsQuery);

        public IEnumerable<AdminApartmentsDto> Execute(AdminApartmentsSearchDto search)
        {
            var apartments = Context.Apartments.Include(x => x.User)
                                               .Include(x => x.CityCountry)
                                               .ThenInclude(x => x.City)
                                               .Include(x => x.Bookings).AsQueryable();

            if (search.UserId.HasValue)
            {
                if(search.UserId.Value != 0)
                {
                    apartments = apartments.Where(x => x.User.Id == search.UserId.Value);
                }
            }

            if(search.TotalBookings.HasValue)
            {
                if(search.TotalBookings.Value != 0)
                {
                    var totalBookings = Common.Extensions.GetTotalBookingsFilter().FirstOrDefault(x => x.Id == search.TotalBookings.Value);
                    var minBookings = int.Parse(totalBookings.Name.Replace("+", ""));

                    apartments = apartments.Where(x => x.Bookings.Count(b => b.IsActive) >= minBookings);
                }
            }

            if(search.CityId.HasValue)
            {
                if(search.CityId.Value != 0)
                {
                    apartments = apartments.Where(x => x.CityCountry.CityId == search.CityId.Value);
                }
            }

            if(search.Status.HasValue)
            {
                int status = search.Status.Value;
                if (status != 0)
                {
                    apartments = apartments.Where(x =>
                    (status == (int)ApartmentStatus.Archived && x.IsArchived == true) ||
                    (status == (int)ApartmentStatus.Active && x.IsActive && x.IsArchived != true) ||
                    (status == (int)ApartmentStatus.Deleted && !x.IsActive && x.IsArchived != true));
                }
            }

            var response = apartments.Select(x => new AdminApartmentsDto
            {
                Id = x.Id,
                City = x.CityCountry.City.Name,
                Name = x.Name,
                Image = x.MainImage,
                Price = x.Price,
                OwnerFullName = x.User.FirstName + " " + x.User.LastName,
                TotalBookings = x.Bookings.Where(x => x.IsActive).Count(),
                Status = (int)(x.IsArchived == true ? ApartmentStatus.Archived : x.IsActive ? ApartmentStatus.Active : ApartmentStatus.Deleted)
            });

            return response;
        }
    }
}
