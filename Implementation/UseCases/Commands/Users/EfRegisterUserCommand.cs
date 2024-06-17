using Application.UseCases.Commands.Users;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using App.Domain;
using Application.DTO.Users;


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

        public string Name => nameof(EfRegisterUserCommand);

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            if(data.Avatar  != null)
            {
                var tmpFile = Path.Combine("wwwroot", "temp", data.Avatar);
                var destinationFile = Path.Combine("wwwroot", "users", data.Avatar);
                File.Move(tmpFile, destinationFile);
            }
            else
            {
                data.Avatar = "default.jpg";
            }

            //7 - Delete user
            //8 - Update user
            //9 - Create Apartment
            //17 - Delete Apartment
            //18 - Update Apartment
            //19 - Update Apartment Images
            User user = new User
            {
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                FirstName = data.FirstName,
                LastName = data.LastName,
                Phone = data.Phone,
                Avatar = data.Avatar,
                UseCases = new List<UserUseCase>()
                {
                    new UserUseCase { UseCaseId = 7},
                    new UserUseCase { UseCaseId = 8},
                    new UserUseCase { UseCaseId = 9},
                    new UserUseCase { UseCaseId = 17},
                    new UserUseCase { UseCaseId = 18},
                    new UserUseCase { UseCaseId = 19},
                }
            };

            Context.Add(user);
            Context.SaveChanges();
        }
    }
}
