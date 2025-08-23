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
            //20 - Create Booking
            //21 - Update Booking
            //22 - Delete Booking
            //23 - Get Bookings
            //24 - Find Booking
            //33 - Change profile photo
            //34 - Send message
            //35 - User Chat List
            //36 - Chat Messages
            //37 - My Guest Bookings
            //38 - Prepare Chat
            //39 - Add Apartment To Favorite
            //40 - Get Favorite Apartments
            //41 - Create Rating
            //42 - Get Ratings
            //43 - Archive Apartment
            //44 - Get Archived Apartments

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
                    new UserUseCase { UseCaseId = 20},
                    new UserUseCase { UseCaseId = 21},
                    new UserUseCase { UseCaseId = 22},
                    new UserUseCase { UseCaseId = 23},
                    new UserUseCase { UseCaseId = 24},
                    new UserUseCase { UseCaseId = 33},
                    new UserUseCase { UseCaseId = 34},
                    new UserUseCase { UseCaseId = 35},
                    new UserUseCase { UseCaseId = 36},
                    new UserUseCase { UseCaseId = 37},
                    new UserUseCase { UseCaseId = 38},
                    new UserUseCase { UseCaseId = 39},
                    new UserUseCase { UseCaseId = 40},
                    new UserUseCase { UseCaseId = 41},
                    new UserUseCase { UseCaseId = 42},
                    new UserUseCase { UseCaseId = 43},
                    new UserUseCase { UseCaseId = 44},
                    new UserUseCase { UseCaseId = 45},
                }
            };

            Context.Add(user);
            Context.SaveChanges();
        }
    }
}
