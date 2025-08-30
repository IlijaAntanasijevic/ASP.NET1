using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using App.Domain;
using Application.DTO.Users;
using Implementation.Common;
using Domain;
using System.Threading.Tasks;
using Application.UseCases;
using Application.Common;


namespace Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _emailSender;
        public EfRegisterUserCommand(BookingContext context, RegisterUserValidator validator, IEmailSender emailSender)
            : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public int Id => 2;

        public string Name => nameof(EfRegisterUserCommand);

        public void Execute(RegisterUserDto data)
        {
            ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task ExecuteInternal(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            if (data.Avatar != null)
            {
                var tmpFile = Path.Combine("wwwroot", "temp", data.Avatar);
                var destinationFile = Path.Combine("wwwroot", "users", data.Avatar);
                File.Move(tmpFile, destinationFile);
            }
            else
            {
                data.Avatar = "default.jpg";
            }

            User user = new User
            {
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                FirstName = data.FirstName,
                LastName = data.LastName,
                Phone = data.Phone,
                Avatar = data.Avatar,
                IsActive = false,
                UseCases = Extensions.AddUserUseCases()
            };

            Context.Add(user);

            var confirmationCode = new Random().Next(100000, 999999).ToString(); //6 random

            var emailConfirmation = new EmailConfirmation
            {
                User = user,
                Code = confirmationCode,
                Expire = DateTime.Now.AddMinutes(2)
            };

            Context.EmailConfirmations.Add(emailConfirmation);

            Context.SaveChanges();

            await _emailSender.ConfirmRegistrationAsync(user.Email, confirmationCode);
        }
    }

}
