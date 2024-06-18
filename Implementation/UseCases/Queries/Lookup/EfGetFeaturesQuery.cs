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
    public class EfGetFeaturesQuery : EfUseCase, IGetFeaturesQuery
    {
        public EfGetFeaturesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 28;

        public string Name => nameof(EfGetFeaturesQuery);

        public IEnumerable<BasicDto> Execute(BasicSearch search)
        {
            return Context.Features.AsQueryable()
                                   .ApplySearch(x => x.Name, search)
                                   .ToList();
        }
    }
}
