using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfConfirmEmailCommand : EfUseCase, IConfirmEmailCommand
    {
        public EfConfirmEmailCommand(BookingContext context) 
            : base(context)
        {
        }

        public int Id => 46;

        public string Name => nameof(EfConfirmEmailCommand);

        public void Execute(ConfirmEmailDto data)
        {
            var user = Context.Users.Where(x => x.Email == data.Email).FirstOrDefault();
            var failures = new List<ValidationFailure>();
            //NAPRAVITI EXCEPTION ZA CUSTOM VALIDATION 
            //var failures = new List<ValidationFailure>
            //            {
            //                new ValidationFailure("OldPassword", "The old password is incorrect.")
            //            };
            //throw new ValidationException(failures);
            if (user == null)
            {
                throw new PermissionDeniedException($"User with {data.Email ?? "/"} email not found");
                //throw new ValidationException(failures);
            }

            var confirmation = Context.EmailConfirmations.Where(x => x.UserId == user.Id).FirstOrDefault();

            if(confirmation == null)
            {
                throw new PermissionDeniedException($"User with {data.Email} email not found");
            }

            if(confirmation.Expire < DateTime.Now)
            {
                throw new PermissionDeniedException($"The code has expired");
            }

            if (data.Code != confirmation.Code)
            {
                throw new PermissionDeniedException($"Wrong code");
            }

            user.IsActive = true;
            Context.SaveChanges();
        }
    }
}
