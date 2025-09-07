using Application.DTO.Admin;
using Application.DTO.Search;

namespace Application.UseCases.Queries.Admin
{
    public interface IGetAdminDashboardQuery : IQuery<AdminDashboardDto, BasicSearch>
    {
    }
}
