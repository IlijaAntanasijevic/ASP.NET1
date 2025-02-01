using App.Domain;
using Application;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using FluentValidation.Results;
using Implementation.Validators;


namespace Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validator;
        private readonly IApplicationActor _actor;
        public EfUpdateUserCommand(BookingContext context, UpdateUserValidator validator, IApplicationActor actor) 
            :base(context)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 8;

        public string Name => nameof(EfUpdateUserCommand);

        public void Execute(UpdateUserDto data)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == data.Id && x.IsActive);

            if(data.Id != _actor.Id || user == null)
            {
                throw new EntityNotFoundException(nameof(User), data.Id);
            }

            if (data.OldPassword != null && data.NewPassword != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(data.OldPassword, user.Password))
                {

                    var failures = new List<ValidationFailure>
                        {
                            new ValidationFailure("OldPassword", "The old password is incorrect.")
                        };
                    throw new ValidationException(failures);

                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
            }

            _validator.ValidateAndThrow(data);

            if (!string.IsNullOrEmpty(data.Avatar))
            {
                var tmpFile = Path.Combine("wwwroot", "temp", data.Avatar);
                var destinationFile = Path.Combine("wwwroot", "users", data.Avatar);
                File.Move(tmpFile, destinationFile);
                user.Avatar = data.Avatar;
            }


            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.Email = data.Email;
            //user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            user.Phone = data.Phone;
            //user.Avatar = data.Avatar;

            Context.SaveChanges();


        }
    }
}
