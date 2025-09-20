using Application.DTO;
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
    public class EfGetAdminTestimonialsQuery : EfUseCase, IGetAdminTestimonialsQuery
    {
        public EfGetAdminTestimonialsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 67;

        public string Name => nameof(EfGetAdminTestimonialsQuery);

        public IEnumerable<AdminTestimonialsDto> Execute(int search)
        {
            var testimonials = Context.Ratings.Include(x => x.Apartment)
                                             .ThenInclude(a => a.CityCountry)
                                             .ThenInclude(x => x.City)
                                             .Include(x => x.Apartment)
                                             .ThenInclude(x => x.CityCountry)
                                             .ThenInclude(x => x.Country)
                                             .Include(x => x.User)
                                             .Include(x => x.ApartmentRatings);

            var response = testimonials.Select(x => new AdminTestimonialsDto
            {
                Id = x.Id,
                ApartmentId = x.ApartmentId,
                IsVisibleOnHome = x.IsVisibleOnHome,
                ApartmentName = x.Apartment.Name,
                UserFullName = x.User.FirstName + " " + x.User.LastName,
                Avatar = x.User.Avatar,
                Location = x.Apartment.CityCountry.City.Name + ", " + x.Apartment.CityCountry.Country.Name,
                Created = x.Date.ToString("MMMM,yyyy"),
                RatingInfo = x.Message,
                Rating = x.ApartmentRatings.Average(r => r.StarRating),
            }).ToList();

            return response;
        }
    }
}
