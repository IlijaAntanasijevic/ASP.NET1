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
    //CHANGE DTO!!!!!!!!!!!!!!!!!! IFindBookingQuery

    public class EfFindBookingQuery : EfUseCase, IFindBookingQuery
    {
        private readonly IApplicationActor _actor;
        public EfFindBookingQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 24;

        public string Name => nameof(EfFindBookingQuery);

        public BasicDto Execute(NamedDto search)
        {
            throw new NotImplementedException();
        }
    }
}
