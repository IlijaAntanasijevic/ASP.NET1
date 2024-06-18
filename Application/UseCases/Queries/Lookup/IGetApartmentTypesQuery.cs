using Application.DTO;
using Application.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Lookup
{
    public interface IGetApartmentTypesQuery : IQuery<IEnumerable<BasicDto>, BasicSearch>
    {
    }
}
