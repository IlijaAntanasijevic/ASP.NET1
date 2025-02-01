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

        public PagedResponseApartment<SearchApartmentsDto> Execute(ApartmentSearch search)
        {
            var query = Context.Apartments.AsQueryable();
            string url = new Uri($"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First()}").AbsoluteUri;

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            if (search.ApartmentTypeIds.Any())
            {
                query = query.Where(x => search.ApartmentTypeIds.Contains(x.ApartmentTypeId));
            }

            if(search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= search.MaxPrice.Value);
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
                var sortProp = search.Sorts.Select(x => x.SortProperty).First();
                var sortDirc = search.Sorts.Select(x => x.Direction).First();

                switch (sortProp)
                {
                    case SortProperty.Price:
                        if(sortDirc == SortDirection.Desc)
                        {
                            query = query.OrderByDescending(x => x.Price);
                            break;
                        }
                        query = query.OrderBy(x => x.Price);
                        break;

                    case SortProperty.MostPopular:
                        break;
                }
                //if(search.Sorts.Any(x => x.SortProperty == "price"))
                //{
                //    if(search.Sorts.FirstOrDefault(x => x.SortProperty == "price").Direction == SortDirection.Asc)
                //    {
                //        query = query.OrderBy(x => x.Price);
                //    }
                //    else
                //    {
                //        query = query.OrderByDescending(x => x.Price);
                //    }
                //}
                if(search.Sorts.Any(x => x.Direction == SortDirection.Desc) && search.Sorts.Any(x => x.SortProperty == null))
                {
                    query = query.OrderByDescending(x => x.CreatedAt);
                }
                if(search.Sorts.Any(x => x.Direction == SortDirection.Asc) && search.Sorts.Any(x => x.SortProperty == null))
                {
                    query = query.OrderBy(x => x.CreatedAt);
                }


            }
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            //decimal maxPrice = query.Select(x => x.Price).Max();
            //decimal minPrice = query.Select(x => x.Price).Min();

            query = query.Skip(skip).Take(perPage);

            var dto = query.Select(x => new SearchApartmentsDto
            {
                Name = x.Name,
                City = x.CityCountry.City.Name,
                Country = x.CityCountry.Country.Name,
                Id = x.Id,
                ApartmentType = x.ApartmentType.Name,
                MainImage = url + x.MainImage.Replace("wwwroot\\", ""),
                MaxGuests = x.MaxGuests,
                PricePerNight = x.Price
            });



            var response = new PagedResponseApartment<SearchApartmentsDto>
            {
                CurrentPage = page,
                Data = dto,
                PerPage = perPage,
                TotalCount = totalCount,
                MaxPrice = null,
                MinPrice = null,
            };

            return response;


            //return query.AsPagedReponse(search, x => new SearchApartmentsDto
            //{
            //    Name = x.Name,
            //    City = x.CityCountry.City.Name,
            //    Country = x.CityCountry.Country.Name,
            //    Id = x.Id,
            //    ApartmentType = x.ApartmentType.Name,
            //    MainImage = url + x.MainImage.Replace("wwwroot\\", ""),
            //    MaxGuests = x.MaxGuests,
            //    PricePerNight = x.Price
            //});


        }
    }
}
