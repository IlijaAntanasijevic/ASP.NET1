using Application.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Admin
{
    public interface IGetAdminBookingsQuery : IQuery<List<AdminBookingsDto>, AdminBookingsSearchDto>
    {
    }
}
