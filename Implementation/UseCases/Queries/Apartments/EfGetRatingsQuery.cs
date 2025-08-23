using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Ratings;
using Application.UseCases.Queries.Apartment;
using Azure;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetRatingsQuery : EfUseCase, IGetRatingsQuery
    {
        public EfGetRatingsQuery(BookingContext context)
            : base(context)
        {
        }

        public int Id => 42;

        public string Name => nameof(EfGetRatingsQuery);

        public PagedResponse<RatingDetailDto> Execute(RatingSearchDto search)
        {
            var ratings = Context.Ratings.Where(x => x.ApartmentId == search.Id)
                                         .Include(x => x.ApartmentRatings)
                                         .ThenInclude(x => x.RatingType)
                                         .Include(x => x.User).OrderByDescending(x => x.Date);


            var response = ratings.AsPagedReponse(search, x => new RatingDetailDto
            {
                Avatar = x.User.Avatar,
                AvgRating = x.ApartmentRatings.Average(a => (float)a.StarRating),
                Comment = x.Message,
                Date = x.Date,
                FullName = x.User.FirstName + " " + x.User.LastName,
            });


            return response;
        }
    }
}
