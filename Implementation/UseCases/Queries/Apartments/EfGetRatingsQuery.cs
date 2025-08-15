using Application.DTO.Ratings;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetRatingsQuery : EfUseCase, IGetRatingsQuery
    {
        public EfGetRatingsQuery(BookingContext context) 
            : base(context)
        {
        }

        public int Id => 42;

        public string Name => nameof(EfGetRatingsQuery);

        public RatingDto Execute(int search)
        {
            throw new NotImplementedException();
        }
    }
}
