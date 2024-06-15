using Application.DTO;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using Domain.Lookup;
using FluentValidation;
using Implementation.Validators;


namespace Implementation.UseCases.Commands.Lookup
{
    public class EfCreateCityCommand : EfUseCase, ICreateCityCommand
    {
        private readonly CreateCityValidator _validator;
        public EfCreateCityCommand(BookingContext context, CreateCityValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 10;

        public string Name => nameof(EfCreateCityCommand);

        public void Execute(NamedDto data)
        {

            _validator.ValidateAndThrow(data);


            var city = new City { Name = data.Name };
            Context.Cities.Add(city);
            Context.SaveChanges();
        }

    }


}
