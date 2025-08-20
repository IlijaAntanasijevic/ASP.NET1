using Application.DTO.Ratings;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateRatingValidator : AbstractValidator<CreateRatingDto>
    {
        public CreateRatingValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ApartmentId).NotEmpty()
                                       .WithMessage("Apartment is required")
                                       .Must(id => context.Apartments.Any(x => x.Id == id && x.IsActive))
                                       .WithMessage("Apartment does't exists.");

            RuleFor(x => x.Comment).NotEmpty()
                                   .WithMessage("Comment is required")
                                   .MinimumLength(5)
                                   .WithMessage("Comment must be at least 5 characters long.");

            RuleFor(x => x.Values).NotEmpty()
                                  .WithMessage("Ratigns is ruqired")
                                  .DependentRules(() =>
                                  {
                                      RuleForEach(x => x.Values).ChildRules(value =>
                                      {
                                          value.RuleFor(x => x.Value)
                                          .InclusiveBetween(1, 5)
                                          .WithMessage("Rating value must be between 1 and 5");

                                          value.RuleFor(x => x.Id)
                                          .Must(id => context.RatingTypes.Any(x => x.Id == id))
                                          .WithMessage("Rating doesn't exists");

                                      });
                                  });
        }

   
    }
}
