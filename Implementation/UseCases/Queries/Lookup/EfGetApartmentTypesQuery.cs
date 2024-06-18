using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.ApartmentType;
using DataAccess;
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

        public IEnumerable<BasicDto> Execute(BasicSearch search)
        {
            return Context.ApartmentTypes.AsQueryable()
                                         .ApplySearch(x => x.Name, search)
                                         .ToList();
        }
    }
}
