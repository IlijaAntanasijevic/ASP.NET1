using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries;
using DataAccess;
using Implementation.Common;
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

        public PagedResponse<ErrorLogsDto> Execute(ErrorLogsSearch search)
        {
            var query = Context.ErrorLogs.OrderByDescending(x => x.Time).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Email.Contains(search.Keyword.ToLower()) || x.ErrorId.ToString().Contains(search.Keyword));
            }

            var response = query.AsPagedReponse(search, x => new ErrorLogsDto
            {
                Email = x.Email,
                ErrorId = x.ErrorId,
                Message = x.Message,
                StackTrace = x.StrackTrace,
                Time = x.Time,
            });

            return response;
        }
    }
}
