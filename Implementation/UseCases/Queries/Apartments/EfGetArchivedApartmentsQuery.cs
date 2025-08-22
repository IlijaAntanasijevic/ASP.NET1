using Application;
using Application.DTO;
using Application.DTO.Apartments;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetArchivedApartmentsQuery : EfUseCase, IGetArchivedApartmentsQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetArchivedApartmentsQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 44;

        public string Name => nameof(EfGetArchivedApartmentsQuery);

        public PagedResponseApartment<SearchApartmentsDto> Execute(BasicApartmantSearch search)
        {
            var data = new List<SearchApartmentsDto>();


            var query = Context.Apartments.Where(x => x.IsArchived.GetValueOrDefault() && x.UserId == _actor.Id)
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
                MaxGuests = x.MaxGuests,
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
