using Application;
using Application.DTO;
using Application.UseCases.Queries.Bookings;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Bookings
{
    //CHANGE DTO!!!!!!!!!!!!!!!!!! IGetBookingsQuery
    public class EfGetBookingsQuery : EfUseCase, IGetBookingsQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetBookingsQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => nameof(EfGetBookingsQuery);

        public BasicDto Execute(NamedDto search)
        {
            throw new NotImplementedException();
        }
    }
}
