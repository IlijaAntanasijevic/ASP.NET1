using Application.DTO;
using Application.DTO.Search;
using Application.UseCases;
using Application.UseCases.Queries.ApartmentType;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.ApartmentType
{
    public class EfGetApartmentTypesQuery : EfUseCase, IGetApartmentTypesQuery
    {
        public EfGetApartmentTypesQuery(BookingContext context) 
            : base(context)
        {
        }

        public int Id => 3;

        public string Name => nameof(EfGetApartmentTypesQuery);

        public List<BasicDto> Execute(BasicSearch search)
        {

           var apartmentTypes = Context.ApartmentTypes.Select(x => new BasicDto { Id = x.Id, Name = x.Name }).ToList();

           return apartmentTypes;
        }
    }
}
