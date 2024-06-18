using App.Domain;
using Application;
using Application.DTO.Apartments;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;


namespace Implementation.UseCases.Commands.Apartments
{
    public class EfUpdateApartmentImagesCommand : EfUseCase, IUpdateApartmentImagesCommand
    {
        private readonly IApplicationActor _actor;
        private readonly IFileUploader _fileUploader;

        public EfUpdateApartmentImagesCommand(BookingContext context, IFileUploader fileUploader, IApplicationActor actor) 
            : base(context)
        {
            _fileUploader = fileUploader;
            _actor = actor;
        }

        public int Id => 19;

        public string Name => nameof(EfUpdateApartmentImagesCommand);

        public void Execute(UpdateApartmentImagesDto data)
        {
           var apartment = Context.Apartments.FirstOrDefault(x => x.Id == data.Id && x.IsActive);

            if(apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartment), data.Id);
            }

            if(apartment.UserId != _actor.Id)
            {
                throw new PermissionDeniedException("You do not have permission to update this apartment.");
            }

            if(data.MainImage != null && data.MainImage != apartment.MainImage)
            {
                apartment.MainImage = _fileUploader.Upload(data.MainImage, UploadType.MainImage);
            }

            if(data.Images.Count()  > 0)
            {
                var images = _fileUploader.Upload(data.Images, UploadType.Apartment);

                apartment.Images = images.Select(x => new Image
                {
                    Path = x
                }).ToList();
            }

            Context.SaveChanges();



        }
    }
}
