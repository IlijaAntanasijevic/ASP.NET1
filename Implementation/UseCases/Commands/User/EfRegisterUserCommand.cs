using Application.DTO;
using Application.UseCases.Commands.User;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Implementation.UseCases.Commands.User
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        protected EfRegisterUserCommand(BookingContext context, RegisterUserValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Register user";

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            //User user = new User
            //{

            //}
        }
    }
}
