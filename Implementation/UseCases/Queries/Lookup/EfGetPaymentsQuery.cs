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
    public class EfGetPaymentsQuery : EfUseCase, IGetPaymentsQuery
    {
        public EfGetPaymentsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 29;

        public string Name => nameof(EfGetPaymentsQuery);

        public IEnumerable<PaymentMethodsDto> Execute(BasicSearch search)
        {
            var payments = Context.Payments.AsQueryable();

            return payments.Select(x => new PaymentMethodsDto
            {
                Id = x.Id,
                Icon = x.Icon,
                ProcessingFee = x.ProcessingFee.GetValueOrDefault(0),
                IsActive = x.IsActive,
                Name = x.Name,
            }).ToList();
        }
    }
}
