using Application.DTO;
using Application.DTO.Admin;
using Application.DTO.Search;
using Application.UseCases.Queries.Admin;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminApartmentsFiltersQuery : EfUseCase, IGetAdminApartmentsFiltersQuery
    {
        public EfGetAdminApartmentsFiltersQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 56;

        public string Name => nameof(EfGetAdminApartmentsFiltersQuery);

        public AdminApartmentsFiltersDto Execute(BasicSearch search)
        {
            var users = Context.Users.Where(x => x.IsActive).Select(x => new BasicDto
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
            }).ToList();

            var totalBookings = new List<BasicDto>
            {
                new BasicDto
                {
                     Id = 1,
                     Name = "5+"
                },
                new BasicDto
                {
                     Id = 2,
                     Name = "10+"
                },
                new BasicDto
                {
                     Id = 3,
                     Name = "25+"
                },
                new BasicDto
                {
                     Id = 4,
                     Name = "50+"
                }
            };

            var cities = Context.Cities.OrderBy(x => x.Name).Select(x => new BasicDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var status = new List<BasicDto>
            {
                new BasicDto
                {
                     Id = 1,
                     Name = "Active"
                },
                new BasicDto
                {
                     Id = 2,
                     Name = "Pending"
                },
                new BasicDto
                {
                     Id = 3,
                     Name = "Deleted"
                }
            };

            var response = new AdminApartmentsFiltersDto
            {
                Cities = cities,
                Statuses = status,
                TotalBookings = totalBookings,
                Users = users,
            };

            return response;
        }
    }
}
