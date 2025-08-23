using App.Domain;
using Application;
using Application.DTO.Apartments;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using Microsoft.EntityFrameworkCore;


namespace Implementation.UseCases.Commands.Apartments
{
    public class EfDeleteApartmentCommand : EfDeleteCommand<Apartment>, IDeleteApartmentCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteApartmentCommand(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public override int Id => 17;

        public override string Name => nameof(EfDeleteApartmentCommand);

        public override void Execute(int id)
        {
            var apartment = Context.Apartments.Include(x => x.Bookings).FirstOrDefault(x => x.Id == id);

            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(apartment), id);
            }

            if (apartment.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("You do not have permission to delete this apartment.");
            }

            var now = DateTime.UtcNow;
            bool hasActiveBookings = apartment.Bookings.Any(x => x.IsActive && x.CheckIn >= now || (x.CheckIn <= now && x.CheckOut >= now));

            if (hasActiveBookings)
            {
                throw new ConflictException("You cannot archive the apartment because it has active or upcoming bookings.");
            }

            base.Execute(id);
        }


    }
}
