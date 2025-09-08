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
                                               .Include(x => x.Bookings);

            var response = apartments.Select(x => new AdminApartmentsDto
            {
                Id = x.Id,
                City = x.CityCountry.City.Name,
                Name = x.Name,
                Image = x.MainImage,
                Price = x.Price,
                OwnerFullName = x.User.FirstName + " " + x.User.LastName,
                TotalBookings = x.Bookings.Where(x => x.IsActive).Count(),
                //Dodati pending
                Status = (int)(x.IsArchived.GetValueOrDefault() ? ApartmentStatus.Archived : x.IsActive ? ApartmentStatus.Active : ApartmentStatus.Deleted)
            });

            return response;
        }
    }
}
