using Application.DTO;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using FluentValidation;
using Implementation.Validators;


namespace Implementation.UseCases.Commands.Lookup.CityCountry
{
    public class EfCreateCityCountryCommand : EfUseCase, ICreateCityCountryCommand
    {
        private readonly CreateCityCountryValidator _validator;
        public EfCreateCityCountryCommand(BookingContext context, CreateCityCountryValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 14;

        public string Name => nameof(EfCreateCityCountryCommand);

        public void Execute(CityCountryDto data)
        {
            _validator.ValidateAndThrow(data);

            var cityCountry = new App.Domain.CityCountry
            {
                CityId = data.CityId,
                CountryId = data.CountryId

            };

            Context.CitiesCountry.Add(cityCountry);
            Context.SaveChanges();
        }
    }
}
