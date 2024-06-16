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
    public class EfCreateFeaturesCommand : EfUseCase, ICreateFeaturesCommand
    {
        private readonly CreateFeaturesValidator _validator;
        public EfCreateFeaturesCommand(BookingContext context, CreateFeaturesValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 12;

        public string Name => nameof(EfCreateFeaturesCommand);

        public void Execute(NamedDto data)
        {
            _validator.ValidateAndThrow(data);

            var feature = new Feature { Name = data.Name };
            Context.Features.Add(feature);
            Context.SaveChanges();
        }
    }
}
