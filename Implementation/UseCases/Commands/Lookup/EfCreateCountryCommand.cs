using Application.DTO;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using Domain.Lookup;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup
{
    public class EfCreateCountryCommand : EfUseCase, ICreateCountryCommand
    {
        private readonly CreateCountryValidator _validator;
        public EfCreateCountryCommand(BookingContext context, CreateCountryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 11;

        public string Name => nameof(EfCreateCountryCommand);

        public void Execute(NamedDto data)
        {
            _validator.ValidateAndThrow(data);

            var country = new Country { Name = data.Name };
            Context.Countries.Add(country);
            Context.SaveChanges();
        }
    }
}
