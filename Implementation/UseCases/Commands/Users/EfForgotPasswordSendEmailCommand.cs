using Application.Common;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain;
using Implementation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfForgotPasswordSendEmailCommand : EfUseCase, IForgotPasswordSendEmailCommand
    {
        private readonly IEmailSender _emailSender;
        public EfForgotPasswordSendEmailCommand(BookingContext context, IEmailSender emailSender)
            : base(context)
        {
            _emailSender = emailSender;
        }

        public int Id => 48;

        public string Name => nameof(EfForgotPasswordSendEmailCommand);

        public void Execute(EmailCodeDto data)
        {
            ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task ExecuteInternal(EmailCodeDto data)
        {
            var user = Context.Users.FirstOrDefault(x => x.Email == data.Email);

            if(user == null)
            {
                throw new ValidationException("User not found");
            }

            var oldEmailConfirmation = Context.EmailConfirmations.FirstOrDefault(x => x.UserId == user.Id);

            if(oldEmailConfirmation != null)
            {
                Context.Remove(oldEmailConfirmation);
            }

            var newCode = new Random().Next(100000, 999999).ToString(); //6 random
            var emailConfirmation = new EmailConfirmation
            {
                User = user,
                Code = newCode,
                Expire = DateTime.Now.AddMinutes(10)
            };

            Context.Add(emailConfirmation);
            Context.SaveChanges();


            await _emailSender.ForgotPasswordAsync(user.Email, newCode);
        }

    }
}
