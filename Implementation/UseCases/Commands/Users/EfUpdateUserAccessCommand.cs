using App.Domain;
using Application.DTO;
using Application.UseCases.Queries.Users;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfUpdateUserAccessCommand : EfUseCase, IUpdateUseAccessCommand
    {
        private UpdateUserAccessValidator _validator;
        public EfUpdateUserAccessCommand(BookingContext context, UpdateUserAccessValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 5;

        public string Name => nameof(EfUpdateUserAccessCommand);

        public void Execute(UpdateUserAccessDto data)
        {
            _validator.ValidateAndThrow(data);

            var userUseCases = Context.UserUseCases.Where(x => x.UserId == data.UserId).ToList();

            Context.UserUseCases.RemoveRange(userUseCases);

            var useCassesToAdd = data.UseCaseIds.Select(x => new UserUseCase
            {
                UserId = data.UserId,
                UseCaseId = x
            });

            Context.UserUseCases.AddRange(useCassesToAdd);

            Context.SaveChanges();
        }
    }
}
