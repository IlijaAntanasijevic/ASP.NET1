using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries;
using DataAccess;
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

        public IEnumerable<UseCaseLogsDto> Execute(BasicSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if(!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Email.Contains(search.Keyword));
            }

            return query.Select(x => new UseCaseLogsDto
            {
                Id = x.Id,
                Email = x.Email,
                ExecutedAt = x.ExecutedAt,
                UseCaseData = x.UseCaseData,
                UseCaseName = x.UseCaseName
            }).ToList();
        }
    }
}
