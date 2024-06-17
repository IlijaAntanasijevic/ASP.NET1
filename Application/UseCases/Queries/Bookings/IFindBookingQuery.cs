using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Bookings
{
    //CHANGE DTO!!!!!!!!!!!!!!!!!! IFindBookingQuery

    public interface IFindBookingQuery : IQuery<BasicDto, NamedDto>
    {
    }
}
