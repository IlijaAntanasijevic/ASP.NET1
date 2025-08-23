using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfArchiveApartmentCommand : EfUseCase, IArchiveApartmentCommand
    {
        private readonly IApplicationActor _actor;
        public EfArchiveApartmentCommand(BookingContext context, IApplicationActor actor)
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 43;

        public string Name => nameof(EfArchiveApartmentCommand);

        public void Execute(int id)
        {
            var apartment = Context.Apartments.Include(x => x.Bookings).FirstOrDefault(x => x.Id == id && x.UserId == _actor.Id && x.IsActive && !x.IsArchived.Value);
            
            if(apartment == null)
            {
                throw new ConflictException("You cannot archive the apartment.");
            }

            //var now = DateTime.UtcNow;
            //bool hasActiveBookings = apartment.Bookings.Any(x => x.IsActive && x.CheckIn >= now || (x.CheckIn <= now && x.CheckOut >= now));
            
            //if (hasActiveBookings)
            //{
            //    throw new ConflictException("You cannot archive the apartment because it has active or upcoming bookings.");
            //}

            apartment.IsArchived = true;
            Context.SaveChangesAsync();
        }
    }
}
