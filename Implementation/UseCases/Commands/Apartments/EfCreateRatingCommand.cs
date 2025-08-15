using Application;
using Application.DTO.Ratings;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfCreateRatingCommand : EfUseCase, ICreateRatingCommand
    {
        private readonly IApplicationActor _actor;
        public EfCreateRatingCommand(BookingContext context, IApplicationActor actor)
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 41;

        public string Name => nameof(EfCreateRatingCommand);

        public void Execute(CreateRatingDto data)
        {
            //VALIDATOR + CREATE
            throw new NotImplementedException();
        }
    }
}
