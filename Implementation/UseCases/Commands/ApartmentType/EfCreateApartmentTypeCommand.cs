using Application.DTO;
using Application.UseCases.Commands.ApartmentType;
using DataAccess;
using FluentValidation;
using Implementation.Validators;

namespace Implementation.UseCases.Commands.ApartmentType
{
    public class EfCreateApartmentTypeCommand : EfUseCase, ICreateApartmentTypeCommand
    {
        private readonly CreateApartmentTypeValidator _validator;

        public EfCreateApartmentTypeCommand(BookingContext context, CreateApartmentTypeValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Insert apartment type";

        public void Execute(NamedDto data)
        {
            _validator.ValidateAndThrow(data);

            var type = new Domain.Lookup.ApartmentType
            {
                Name = data.Name,
            };

            Context.ApartmentTypes.Add(type);
            Context.SaveChanges();
        }
    }
}
