using Application.DTO;
using Application.DTO.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Apartment
{
    public interface IGetRatingsQuery : IQuery<PagedResponse<RatingDetailDto>, RatingSearchDto>
    {
    }
}
