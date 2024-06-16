using Application.DTO.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.Apartments
{
    public interface ICreateApartmentCommand : ICommand<CreateApartmentDto>
    {
    }
}
