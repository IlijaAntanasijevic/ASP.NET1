using Application.DTO;
using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfCreateApartmentCommand : EfUseCase, ICreateApartmentCommand
    {
        public EfCreateApartmentCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => nameof(EfCreateApartmentCommand);

        //CHANGE DTO
        public void Execute(ApartmentDto data)
        {
            throw new NotImplementedException();
        }
    }
}
