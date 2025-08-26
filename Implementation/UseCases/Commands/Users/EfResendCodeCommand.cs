using Application.Common;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Commands.Users
{
    public class EfResendCodeCommand : EfUseCase, IResendCodeCommand
    {
        private readonly IEmailSender _emailSender;
        public EfResendCodeCommand(BookingContext context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

        public int Id => 47;

        public string Name => nameof(EfResendCodeCommand);

        public void Execute(EmailCodeDto data)
        {
            ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task ExecuteInternal(EmailCodeDto data)
        {
            var user = Context.Users.Where(x => x.Email == data.Email).FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException($"User with {data.Email ?? "/"} email not found");
            }

            var confirmation = Context.EmailConfirmations.Where(x => x.UserId == user.Id).FirstOrDefault();

            if (confirmation != null)
            {
                Context.Remove(confirmation);
            }

            var newCode = new Random().Next(100000, 999999).ToString(); //6 random
            var emailConfirmation = new EmailConfirmation
            {
                User = user,
                Code = newCode,
                Expire = DateTime.Now.AddMinutes(5)
            };

            Context.Add(emailConfirmation);
            Context.SaveChanges();

            //await _emailSender.SendEmailConfirmRegistrationAsync(user.Email, newCode);
            await _emailSender.SendEmailConfirmRegistrationAsync("ilija0308@gmail.com", newCode);
        }
    }
}
