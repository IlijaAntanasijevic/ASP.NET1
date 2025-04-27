using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using App.Domain;
using Application;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Implementation.Validators;
using FluentValidation;
using Mapster;
namespace Implementation.UseCases.Commands.Apartments
{
    public class EfUpdateApartmentCommand : EfUseCase, IUpdateApartmentCommand
    {
        private readonly IApplicationActor _actor;
        private readonly UpdateApartmentValidator _validator;
        private readonly IFileUploader _fileUploader;

        public EfUpdateApartmentCommand(BookingContext context, IApplicationActor actor, UpdateApartmentValidator validator, IFileUploader fileUploader)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
            _fileUploader = fileUploader;
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

            TypeAdapterConfig<UpdateApartmentDto, Apartment>.NewConfig()
                .Ignore(x => x.FeatureApartments)
                .Ignore(x => x.PaymentApartments)
                .Ignore(x => x.CityCountry)
                .Ignore(x => x.Images);

            data.Adapt(apartment);
            //apartment.Name = data.Name;
            //apartment.Description = data.Description;
            //apartment.Price = data.Price;
            ////apartment.MaxGuests = data.MaxGuests;
            //apartment.Price = data.Price;

            Context.RemoveRange(apartment.FeatureApartments);
            Context.RemoveRange(apartment.PaymentApartments);

            var cityCountry = Context.CitiesCountry.FirstOrDefault(x => x.CityId == data.CityId && x.CountryId == data.CountryId);

            //slike koje nisu stigle sa fronta treba obrisati 
            //Promeni u bazi PATH u NAME/ImageName..
            var apartmentImages = Context.Images.Where(x => x.ApartmentId == data.Id).ToList();
            var imagesNotExists = apartmentImages.Where(x => data.Images.Contains(x.Path));

            if (imagesNotExists.Any())
            {
                var imagesToAdd = data.Images.Where(x => !apartmentImages.Any(y => y.Path == x)).ToList();
                _fileUploader.MoveImages(imagesToAdd, UploadType.Apartment);
                //foreach(var image in imagesNotExists)
                //{
                //    _fileUploader.MoveImages(data.Images, UploadType.Apartment);
                //    //Context.Remove(image);
                //}
            }

            if(cityCountry == null)
            {
                throw new ArgumentNullException();
            }

            apartment.CityCountry = cityCountry;

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
