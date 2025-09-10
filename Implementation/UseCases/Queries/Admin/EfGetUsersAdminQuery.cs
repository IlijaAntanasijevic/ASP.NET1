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
    public class EfGetUsersAdminQuery : EfUseCase, IGetUsersAdminQuery
    {
        public EfGetUsersAdminQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 57;

        public string Name => nameof(EfGetUsersAdminQuery);

        public List<AdminUsersDto> Execute(BasicSearch search)
        {
            var users = Context.Users.AsQueryable();
            if(search != null && search.Keyword != null)
            {
                string keyword = search.Keyword.ToLower();
                users = users.Where(x => x.FirstName.ToLower().Contains(keyword) || x.LastName.ToLower().Contains(keyword) || x.Email.ToLower().Contains(keyword));
            }

            var response = users.Select(x => new AdminUsersDto
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FirstName + " " + x.LastName,
                Phone = x.Phone,
                Avatar = x.Avatar,
                TotalApartments = x.Apartments.Count(),
                TotalBookings = x.Bookings.Count(b => b.IsActive),
            }).ToList();

            return response;
        }
    }
}
