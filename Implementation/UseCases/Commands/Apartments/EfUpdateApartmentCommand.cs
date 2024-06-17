using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using Implementation.Exceptions;
using Domain;
using App.Domain;
using Application;
using DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
namespace Implementation.UseCases.Commands.Apartments
{
    public class EfUpdateApartmentCommand : EfUseCase, IUpdateApartmentCommand
    {
        private readonly IApplicationActor _actor;
     
        public EfUpdateApartmentCommand(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;

        }

        public int Id => 18;

        public string Name => nameof(EfUpdateApartmentCommand);

        public void Execute(UpdateApartmentDto data)
        {
            var apartment = Context.Apartments.Include(x => x.User)
                                              .Include(x => x.FeatureApartments)
                                              .Include(x => x.PaymentApartments)
                                              .FirstOrDefault(x => x.Id == data.Id && x.IsActive);

            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartment), data.Id);
            }

            if(apartment.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("You do not have permission to update this apartment.");
            }

            apartment.Name = data.Name;
            apartment.Description = data.Description;
            apartment.MaxGuests = data.MaxGuests;
            apartment.Price = data.Price;

            Context.RemoveRange(apartment.FeatureApartments);
            Context.RemoveRange(apartment.PaymentApartments);

       
            apartment.FeatureApartments = data.FeatureIds.Select(x => new FeatureApartment
            {
                FeatureId = x,
                ApartmentId = apartment.Id 
            }).ToList();

            apartment.PaymentApartments = data.PaymentMethodIds.Select(x => new PaymentApartment
            {
                PaymentId = x,
                ApartmentId = apartment.Id 
            }).ToList();


            Context.SaveChanges();


        }
    }
}
