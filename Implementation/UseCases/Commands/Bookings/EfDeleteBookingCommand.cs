using Application;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfDeleteBookingCommand : EfUseCase, IDeleteBookingCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteBookingCommand(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => nameof(EfDeleteBookingCommand);

        public void Execute(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
