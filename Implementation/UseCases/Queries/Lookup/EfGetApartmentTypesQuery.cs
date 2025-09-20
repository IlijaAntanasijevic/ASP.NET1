using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Lookup;
using DataAccess;
using Implementation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Lookup
{
    public class EfGetApartmentTypesQuery : EfUseCase, IGetApartmentTypesQuery
    {
        public EfGetApartmentTypesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => nameof(EfGetApartmentTypesQuery);

        public IEnumerable<LookupDto> Execute(BasicSearch search)
        {
            return Context.ApartmentTypes.Where(x => search.IsActive ?? true ? x.IsActive : true).Select(x => new LookupDto
            {
                Id = x.Id,
                Name = x.Name,
                Icon = x.Icon,
                IsActive = x.IsActive,
            }).ToList();
        }
    }
}
