using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Bookings;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Bookings
{
    public class EfGetMyGuestBookingsQuery : EfUseCase, IGetMyGuestBookingsQuery
    {
        public EfGetMyGuestBookingsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => nameof(EfGetMyGuestBookingsQuery);

        public PagedResponse<SearchedBookingDto> Execute(BookingSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
