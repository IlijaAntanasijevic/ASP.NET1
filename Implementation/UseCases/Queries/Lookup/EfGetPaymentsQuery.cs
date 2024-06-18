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
    public class EfGetPaymentsQuery : EfUseCase, IGetPaymentsQuery
    {
        public EfGetPaymentsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 29;

        public string Name => nameof(EfGetPaymentsQuery);

        public IEnumerable<BasicDto> Execute(BasicSearch search)
        {
            return Context.Payments.AsQueryable()
                                   .ApplySearch(x => x.Name, search)
                                   .ToList();
        }
    }
}
