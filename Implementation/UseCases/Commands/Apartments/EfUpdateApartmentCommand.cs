using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using App.Domain;
using Application;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Implementation.Validators;
using FluentValidation;
namespace Implementation.UseCases.Commands.Apartments
{
    public class EfUpdateApartmentCommand : EfUseCase, IUpdateApartmentCommand
    {
        private readonly IApplicationActor _actor;
        private readonly UpdateApartmentValidator _validator;

        public EfUpdateApartmentCommand(BookingContext context, IApplicationActor actor, UpdateApartmentValidator validator)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 18;

        public string Name => nameof(EfUpdateApartmentCommand);

        public void Execute(UpdateApartmentDto data)
        {

            _validator.ValidateAndThrow(data);
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
