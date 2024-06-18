using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetErrorLogsQuery : EfUseCase, IGetErrorLogsQuery
    {
        public EfGetErrorLogsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => nameof(EfGetErrorLogsQuery);

        public IEnumerable<ErrorLogsDto> Execute(BasicSearch search)
        {
            var query = Context.ErrorLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Email.Contains(search.Keyword.ToLower()) || x.Message.Contains(search.Keyword.ToLower()));
            }

            return query.Select(x => new ErrorLogsDto
            {
                Email = x.Email,
                ErrorId = x.ErrorId,
                Message = x.Message,
                StrackTrace = x.StrackTrace,
                Time = x.Time,
            }).ToList();
        }
    }
}
