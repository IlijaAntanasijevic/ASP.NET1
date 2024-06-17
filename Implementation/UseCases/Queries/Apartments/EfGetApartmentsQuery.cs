using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetApartmentsQuery : EfUseCase, IGetApartmentsQuery
    {
        public EfGetApartmentsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => nameof(EfGetApartmentsQuery);

        public PagedResponse<SearchApartmentsDto> Execute(ApartmentSearch search)
        {
            var query = Context.Apartments.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            if (search.ApartmentTypeId.HasValue)
            {
                query = query.Where(x => x.ApartmentTypeId == search.ApartmentTypeId.Value);
            }

            if(search.CountryId.HasValue)
            {
                query = query.Where(x => x.CityCountry.CountryId == search.CountryId.Value);
            }
            if (search.CityId.HasValue)
            {
                query = query.Where(x => x.CityCountry.CityId == search.CityId.Value);
            }

            if (search.Sorts != null && !search.Sorts.Any())
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                if(search.Sorts.Any(x => x.SortProperty == "price"))
                {
                    if(search.Sorts.FirstOrDefault(x => x.SortProperty == "price").Direction == SortDirection.Asc)
                    {
                        query = query.OrderBy(x => x.Price);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.Price);
                    }
                }
            }

            return query.AsPagedReponse(search, x => new SearchApartmentsDto
            {
                Name = x.Name,
                City = x.CityCountry.City.Name,
                Country = x.CityCountry.Country.Name,
                Id = x.Id,
                ApartmentType = x.ApartmentType.Name,
                MainImage = x.MainImage,
                MaxGuests = x.MaxGuests,
                PricePerNight = x.Price
            });


        }
    }
}
