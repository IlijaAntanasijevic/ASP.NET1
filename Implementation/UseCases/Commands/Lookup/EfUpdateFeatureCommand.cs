using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup
{
    public class EfUpdateFeatureCommand : EfUseCase, IUpdateFeatureCommand
    {
        public EfUpdateFeatureCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 64;

        public string Name => nameof(EfUpdateFeatureCommand);

        public void Execute(LookupDto data)
        {
            var feature = Context.Features.FirstOrDefault(x => x.Id == data.Id);
            if (feature == null)
            {
                throw new EntityNotFoundException(nameof(Feature), data.Id ?? 0);
            }

            feature.Name = data.Name;
            feature.IsActive = data.IsActive ?? false;
            feature.Icon = data.Icon;

            Context.SaveChanges();
        }
    }
}
