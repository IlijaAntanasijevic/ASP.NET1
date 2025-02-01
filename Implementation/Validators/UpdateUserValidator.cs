using Application.DTO.Users;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly BookingContext _context;
        public UpdateUserValidator(BookingContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email address is not in a valid format.")
                .Must((dto, email) => !context.Users.Any(u => u.Email == email && u.Id != dto.Id))
                .WithMessage("Email is already in use.");

            RuleFor(x => x.NewPassword)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")
                .WithMessage("Minimum eight characters, at least one letter, one number, and one special character");

            RuleFor(x => x.FirstName)
                .MinimumLength(3)
                .WithMessage("First name must be at least 3 characters long.");

            RuleFor(x => x.LastName)
                .MinimumLength(3)
                .WithMessage("Last name must be at least 3 characters long.");

            RuleFor(x => x.Phone)
                .MaximumLength(15)
                .WithMessage("Phone number must not exceed 15 characters.")
                .Must((dto, phone) => !context.Users.Any(u => u.Phone == phone && u.Id != dto.Id))
                .WithMessage("Phone number is already in use.");

            RuleFor(x => x.Avatar)
                .Must(FindFile)
                .When(x => !string.IsNullOrEmpty(x.Avatar))
                .WithMessage("File doesn't exist.");
        }

        private bool FindFile(string fileName)
        {
            var path = Path.Combine("wwwroot", "temp", fileName);
            return File.Exists(path);
        }

    
    }
}
