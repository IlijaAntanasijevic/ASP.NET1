using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfActivateApartmentCommand : EfUseCase, IActivateApartmentCommand
    {
        private readonly IApplicationActor _actor;
        public EfActivateApartmentCommand(BookingContext context, IApplicationActor actor)
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 45;

        public string Name => nameof(EfActivateApartmentCommand);

        public void Execute(int id)
        {
            var apartment = Context.Apartments.FirstOrDefault(x => x.Id == id);

            if(apartment == null)
            {
                throw new EntityNotFoundException(nameof(apartment), id);
            }

            if (apartment.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("You do not have permission to activate this apartment.");
            }

            apartment.IsArchived = false;
            Context.SaveChanges();

        }
    }
}
