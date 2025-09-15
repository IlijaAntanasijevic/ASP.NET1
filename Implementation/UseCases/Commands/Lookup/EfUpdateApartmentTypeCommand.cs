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
    public class EfUpdateApartmentTypeCommand : EfUseCase, IUpdateApartmentTypeCommand
    {
        public EfUpdateApartmentTypeCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 63;

        public string Name => nameof(EfUpdateApartmentTypeCommand);

        public void Execute(LookupDto data)
        {
            var type = Context.ApartmentTypes.FirstOrDefault(x => x.Id == data.Id);
            if(type == null)
            {
                throw new EntityNotFoundException(nameof(ApartmentType), data.Id ?? 0);
            }

            type.Name = data.Name;
            type.Icon = data.Icon;
            type.IsActive = data.IsActive ?? false;

            Context.SaveChanges();

        }
    }
}
