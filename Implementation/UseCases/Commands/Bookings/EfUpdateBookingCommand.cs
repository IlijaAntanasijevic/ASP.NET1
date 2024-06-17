using Application;
using Application.DTO.Bookings;
using Application.UseCases.Commands.Bookings;
using DataAccess;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Bookings
{
    public class EfUpdateBookingCommand : EfUseCase, IUpdateBookingCommand
    {
        private readonly IApplicationActor _actor;
        private readonly UpdateBookingValidator _validator;
        public EfUpdateBookingCommand(BookingContext context, IApplicationActor actor, UpdateBookingValidator validator)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 21;

        public string Name => nameof(EfUpdateBookingCommand);

        public void Execute(EditBookingDto data)
        {
            throw new NotImplementedException();
        }
    }
}
