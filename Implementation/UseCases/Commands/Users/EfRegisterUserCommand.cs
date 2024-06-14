using Application.DTO;
using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using App.Domain;


namespace Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        public EfRegisterUserCommand(BookingContext context, RegisterUserValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Register user";

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                FirstName = data.FirstName,
                LastName = data.LastName,
                Phone = data.Phone,
                Avatar = data.Avatar
            };

            Context.Add(user);
            Context.SaveChanges();
        }
    }
}
