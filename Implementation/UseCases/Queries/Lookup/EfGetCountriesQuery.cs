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
    public class EfGetCountriesQuery : EfUseCase, IGetCountriesQuery
    {
        public EfGetCountriesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 26;

        public string Name => nameof(EfGetCountriesQuery);

        public IEnumerable<BasicDto> Execute(BasicSearch search)
        {
            return Context.Countries.AsQueryable()
                                    .ApplySearch(x => x.Name, search)
                                    .ToList();
        }
    }
}
