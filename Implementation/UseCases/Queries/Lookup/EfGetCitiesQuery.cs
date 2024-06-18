using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Lookup;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Lookup
{
    public class EfGetCitiesQuery : EfUseCase, IGetCitiesQuery
    {
        public EfGetCitiesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 25;

        public string Name => nameof(EfGetCitiesQuery);

        public IEnumerable<BasicDto> Execute(BasicSearch search)
        {
            return Context.Cities.AsQueryable()
                                 .ApplySearch(x => x.Name, search)
                                 .ToList();

        }
    }
}
