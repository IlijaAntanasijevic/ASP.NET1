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
                                         .Include(x => x.User);


            //var response = ratings.Select(x => new RatingDto
            //{
            //    AvgRating = ratings.SelectMany(r => r.ApartmentRatings).Average(a => (float)a.StarRating),
            //    Ratings = 
            //})

            var response = ratings.AsPagedReponse(search, x => new RatingDetailDto
            {
                 Avatar = x.User.Avatar,
                  AvgRating = x.ApartmentRatings.Average(a => (float)a.StarRating),
                   Comment = x.Message,
                    Date = x.Date,
                     FullName = x.User.FirstName + " " + x.User.LastName,
            });


            return response;

            //var dto = new RatingDetailDto();
            //dto.AvgRating = ratings.SelectMany(r => r.ApartmentRatings).Average(a => (float)a.StarRating);

            //dto.Ratings = ratings.Select(x => new RatingDetailDto
            //{
            //    Date = x.Date.ToString("yyyy-MM-dd"),
            //    FullName = x.User.FirstName + " " + x.User.LastName,
            //    Comment = x.Message,
            //    AvgRating = x.ApartmentRatings.Average(ar => (float)ar.StarRating),
            //    Avatar = x.User.Avatar
            //}).ToList();

            //dto.Values = ratings
            //        .SelectMany(r => r.ApartmentRatings) 
            //        .GroupBy(ar => new { ar.RatingTypeId, ar.RatingType.Name })
            //        .Select(g => new RaingValuesDto
            //        {
            //            Id = g.Key.RatingTypeId,
            //            Value = (float)Math.Round(g.Average(ar => ar.StarRating), 1)
            //        }).ToList();


            //int totalCount = ratings.Count();

            //int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            //int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            //var tmp = new PagedResponseApartment<RatingDto>
            //{
            //    CurrentPage = page,
            //    Data = dto,
            //    PerPage = perPage,
            //    TotalCount = totalCount,
            //    //MaxPrice = null,
            //    //MinPrice = null,
            //};

            //return response;
        }
    }
}
