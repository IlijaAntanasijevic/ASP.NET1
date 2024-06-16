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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteUserCommand(BookingContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 7;

        public string Name => nameof(EfDeleteUserCommand);

        public void Execute(int userId)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == userId && x.IsActive);
 
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), userId);
            }
            if(userId != _actor.Id)
            {
                throw new ConflictException("User can not be deleted");
            }

            user.IsActive = false;
            Context.SaveChanges();

        }
    }
}
