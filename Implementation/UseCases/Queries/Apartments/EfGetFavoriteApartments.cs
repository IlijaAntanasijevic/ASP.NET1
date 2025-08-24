using Application;
using Application.Common;
using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetFavoriteApartments : EfUseCase, IGetFavoriteApartments
    {
        private readonly IApplicationActor _actor;
        public EfGetFavoriteApartments(BookingContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 40;

        public string Name => nameof(EfGetFavoriteApartments);

        public PagedResponseApartment<SearchApartmentsDto> Execute(BasicApartmantSearch search)
        {
            var data = new List<SearchApartmentsDto>();


            var query = Context.Apartments.Where(x => x.Favorites.Any(f => f.UserId == _actor.Id))
                .Include(x => x.CityCountry)
                .ThenInclude(x => x.City)
                .Include(x => x.CityCountry)
                .ThenInclude(x => x.Country)
                .Include(x => x.ApartmentType)
                .Include(x => x.Favorites).AsQueryable();
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            var apartments = query.Skip(skip).Take(perPage).ToList();

            data.AddRange(apartments.Select(x => new SearchApartmentsDto
            {
                Id = x.Id,
                Name = x.Name,
                City = x.CityCountry.City.Name,
                Country = x.CityCountry.Country.Name,
                ApartmentType = x.ApartmentType.Name,
                MainImage = new ApartmentImageDto
                {
                    FileName = x.MainImage,
                    ImageType = UploadType.MainImage,
                    OriginalFileName = null
                },
                //MaxGuests = x.MaxGuests,
                Adults = x.MaxAdults,
                Childrens = x.MaxChildren,
                TotalRooms = x.TotalRooms,
                PricePerNight = x.Price,
                IsFavorite = x.Favorites.Any(f => f.UserId == _actor.Id)
            }));

            return new PagedResponseApartment<SearchApartmentsDto>
            {
                Data = data,
                TotalCount = totalCount,
                PerPage = perPage,
                CurrentPage = page,
            };

        }
    }
}
