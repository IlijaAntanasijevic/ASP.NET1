using Application.DTO;
using DataAccess;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Email is required.")
                                 .EmailAddress()
                                 .WithMessage("Email address is not in a valid format.")
                                 .Must(email => !context.Users.Any(e => e.Email == email))
                                 .WithMessage("Email is already in use.");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required.")
                                    .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")
                                    .WithMessage("Minimum eight characters, at least one letter, one number and one special character");

            RuleFor(x => x.FirstName).NotEmpty()
                                     .WithMessage("First name is required")
                                     .MinimumLength(3)
                                     .WithMessage("First name must be at least 3 characters long.");

            RuleFor(x => x.LastName).NotEmpty()
                                     .WithMessage("Last name is required")
                                     .MinimumLength(3)
                                     .WithMessage("Last name must be at least 3 characters long.");

            RuleFor(x => x.Phone).NotEmpty()
                                 .WithMessage("Phone number is required")
                                 .MaximumLength(15)
                                 .WithMessage("Phone number must not exceed 15 characters.")
                                 .Must(phone => !context.Users.Any(p => p.Phone == phone))
                                 .WithMessage("Phone number is already in use.");

        }
    }
}
