using App.Domain;
using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Bookings;
using DataAccess;


namespace Implementation.UseCases.Commands.Bookings
{
    public class EfDeleteBookingCommand : EfDeleteCommand<Booking>, IDeleteBookingCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteBookingCommand(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public override int Id => 22;

        public override string Name => nameof(EfDeleteBookingCommand);

        public override void Execute(int bookingId)
        {
            var booking = Context.Bookings.FirstOrDefault(x => x.Id == bookingId && x.IsActive);

            if (booking == null)
            {
                throw new EntityNotFoundException(nameof(booking), bookingId);
            }

            if(_actor.Id != booking.UserId)
            {
                throw new PermissionDeniedException("You do not have permission to cancel this booking.");
            }

            //2024-06-20 // 2024-06-21  TODAY = 2024-06-20 
            if(booking.CheckIn <= DateTime.Now.AddDays(1))
            {
                throw new PermissionDeniedException("Booking cannot be caceled within one day of check-in.");
            }

            base.Execute(bookingId);
        }
    }
}
