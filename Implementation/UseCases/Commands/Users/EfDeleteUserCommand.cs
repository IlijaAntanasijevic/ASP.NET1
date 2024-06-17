using App.Domain;
using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfDeleteUserCommand : EfDeleteCommand<User>, IDeleteUserCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteUserCommand(BookingContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }


        public override int Id => 7;
        public override string Name => nameof(EfDeleteUserCommand);

        public override void Execute(int id)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
 
            if(id != _actor.Id)
            {
                throw new ConflictException("User can not be deleted");
            }

           base.Execute(id);

        }
    }
}
