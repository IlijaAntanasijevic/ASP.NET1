using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Apartment
{
    public interface IGetApartmentsQuery : IQuery<PagedResponse<SearchApartmentsDto>, ApartmentSearch>
    {
    }
}
