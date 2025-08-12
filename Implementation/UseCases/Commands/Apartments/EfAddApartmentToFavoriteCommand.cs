using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfAddApartmentToFavoriteCommand : EfUseCase, IAddApartmentToFavoriteCommand
    {
        private readonly IApplicationActor _actor;
        public EfAddApartmentToFavoriteCommand(BookingContext context, IApplicationActor actor)
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 39;

        public string Name => nameof(EfAddApartmentToFavoriteCommand);

        public void Execute(int apartmentId)
        {
            var apartment = Context.Apartments.Where(x => x.Id == apartmentId && (!x.IsArchived.HasValue || !x.IsArchived.Value)).FirstOrDefault();
            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartments), apartmentId);
            }

            var entity = new FavoriteApartments
            {
                ApartmentId = apartmentId,
                UserId = _actor.Id
            };

            Context.Add(entity);
            Context.SaveChanges();
        }
    }
}
