using Application.DTO;
using Application.UseCases.Queries.Home;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Home
{
    public class EfGetHomeTestimonialsQuery : EfUseCase, IGetHomeTestimonialsQuery
    {
        public EfGetHomeTestimonialsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 66;

        public string Name => nameof(EfGetHomeTestimonialsQuery);

        public IEnumerable<HomeTestimonials> Execute(int search)
        {
            var testimonials = Context.Ratings.Where(x => x.IsVisibleOnHome).Include(x => x.Apartment)
                .ThenInclude(a => a.CityCountry)
                .ThenInclude(x => x.City)
                .Include(x => x.Apartment)
                .ThenInclude(x => x.CityCountry)
                .ThenInclude(x => x.Country)
                .Include(x => x.User)
                .Include(x => x.ApartmentRatings);

            var response = testimonials.Select(x => new HomeTestimonials
            {
                Id = x.Id,
                ApartmentId = x.ApartmentId,
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
