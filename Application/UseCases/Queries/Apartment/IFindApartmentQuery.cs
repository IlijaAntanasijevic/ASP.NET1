using Application.DTO.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Apartment
{
    public interface IFindApartmentQuery : IQuery<ApartmentDto, int>
    {
    }
}
