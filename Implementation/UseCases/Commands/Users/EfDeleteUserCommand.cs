using App.Domain;
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
        public EfDeleteUserCommand(BookingContext context) : base(context)
        {
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

            user.IsActive = false;
            Context.SaveChanges();

        }
    }
}
