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
                .Ignore(x => x.Images)
                .Ignore(x => x.MainImage);

            data.Adapt(apartment);

            Context.RemoveRange(apartment.FeatureApartments);
            Context.RemoveRange(apartment.PaymentApartments);

            var cityCountry = Context.CitiesCountry.FirstOrDefault(x => x.CityId == data.CityId && x.CountryId == data.CountryId);

            //slike koje nisu stigle sa fronta treba obrisati 
            //Promeni u bazi PATH u NAME/ImageName..
            if(apartment.MainImage != data.MainImage)
            {
                //Obrisati staru iz foldera
                //Premestiti iz images folder u mainImages
                _fileUploader.MoveImage(data.MainImage, UploadType.MainImage);
                _fileUploader.DeleteImage(apartment.MainImage);
                apartment.MainImage = data.MainImage;
            }

            var apartmentImages = Context.Images.Where(x => x.ApartmentId == data.Id).ToList();
            var imagesToDelete = apartmentImages.Where(x => !data.Images.Any(image => x.Path.Contains(image)) && apartment.MainImage != x.Path).ToList();
            var imagesToAdd = data.Images.Where(x => !apartmentImages.Any(image => image.Path.Contains(x)) && apartment.MainImage != x).ToList();

            foreach(var image in imagesToDelete)
            {
                _fileUploader.DeleteImage(image.Path);
                Context.Images.Remove(image);
            }

            foreach(var image in imagesToAdd)
            {
                _fileUploader.MoveImage(image, UploadType.Apartment);
                Context.Images.Add(new Image
                {
                    ApartmentId = apartment.Id,
                    Path = image
                });
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
