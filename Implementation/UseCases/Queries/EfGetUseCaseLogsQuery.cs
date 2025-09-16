using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries;
using DataAccess;
using Implementation.Common;
using Newtonsoft.Json;


namespace Implementation.UseCases.Queries
{
    public class EfGetUseCaseLogsQuery : EfUseCase, IGetUseCaseLogsQuery
    {
        public EfGetUseCaseLogsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 30;

        public string Name => nameof(EfGetUseCaseLogsQuery);

        public PagedResponse<UseCaseLogsDto> Execute(UseCaseLogsSearch search)
        {
            var query = Context.UseCaseLogs.OrderByDescending(x => x.ExecutedAt).AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword))
            {
                string keywrod = search.Keyword.Trim().ToLower();
                query = query.Where(x => x.Email.ToLower().Contains(keywrod) || x.UseCaseName.ToLower().Contains(keywrod));
            }

            var response = query.AsPagedReponse(search, x => new UseCaseLogsDto
            {
                Id = x.Id,
                Email = x.Email,
                ExecutedAt = x.ExecutedAt,
                UseCaseData = x.UseCaseData,
                UseCaseName = x.UseCaseName
            });

            return response;

        }
    }
}
