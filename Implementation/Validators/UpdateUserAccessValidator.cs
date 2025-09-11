using App.Domain;
using Application.DTO.Users;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UpdateUserAccessValidator : AbstractValidator<UpdateUserAccessDto>
    {
        private static int updateUserAccessId = 5;

        public UpdateUserAccessValidator(BookingContext context)
        {

            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.UserId)
                    .Must(x => context.Users.Any(u => u.Id == x && u.IsActive))
                    .WithMessage("Requested user doesn't exist.");

            RuleFor(x => x.UseCaseIds)
                .Must(x => x.All(id => id > 0 && id <= UseCaseInfo.MaxUseCaseId)).WithMessage("Invalid usecase id range.")
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Only unique usecase ids must be delivered.");



        }
    }
}
