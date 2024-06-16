using App.Domain;
using Application;
using Application.DTO;
using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfCreateApartmentCommand : EfUseCase, ICreateApartmentCommand
    {
        private readonly CreateApartmentValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IFileUploader _fileUploader;
        public EfCreateApartmentCommand(BookingContext context, CreateApartmentValidator validator, IApplicationActor actor, IFileUploader uploader)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
            _fileUploader = uploader;
        }

        public int Id => 9;

        public string Name => nameof(EfCreateApartmentCommand);

        public void Execute(CreateApartmentDto data)
        {
            _validator.ValidateAndThrow(data);

            var filePath = _fileUploader.Upload(data.MainImage, UploadType.MainImage);

            var apartmentToAdd = new Apartment
            {
                UserId = _actor.Id,
                Name = data.Name,
                Description = data.Description,
                Address = data.Address,
                CityCountryId = data.CityCountryId,
                MaxGuests = data.MaxGuests,
                Price = data.Price,
                ApartmentTypeId = data.ApartmentTypeId,
                MainImage = filePath,
                FeatureApartments = data.FeatureIds.Select(x => new FeatureApartment
                {
                    FeatureId = x
                }).ToList(),
                PaymentApartments = data.PaymentMethodIds.Select(x => new PaymentApartment
                {
                     PaymentId = x
                }).ToList()
       
            };

            Context.Apartments.Add(apartmentToAdd);
            Context.SaveChanges();


            
        }
    }
}
