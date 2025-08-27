using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfForgotPasswordCheckCodeCommand : EfUseCase, IForgotPasswordCheckCodeCommand
    {
        public EfForgotPasswordCheckCodeCommand(BookingContext context) 
            : base(context)
        {
        }

        public int Id => 49;

        public string Name => nameof(EfForgotPasswordCheckCodeCommand);

        public void Execute(ConfirmEmailDto data)
        {
            var user = Context.Users.Where(x => x.Email == data.Email).FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException($"User with {data.Email ?? "/"} email not found");
            }

            var confirmation = Context.EmailConfirmations.Where(x => x.UserId == user.Id).FirstOrDefault();

            if (confirmation == null)
            {
                throw new ValidationException($"User with {data.Email} email not found");
            }

            if (confirmation.Expire < DateTime.Now)
            {
                throw new ValidationException($"The code has expired");
            }

            if (data.Code != confirmation.Code)
            {
                throw new ValidationException($"Wrong code");
            }
        }
    }
}
