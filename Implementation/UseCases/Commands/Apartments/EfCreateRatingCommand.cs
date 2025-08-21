using Application;
using Application.DTO.Ratings;
using Application.Exceptions;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfCreateRatingCommand : EfUseCase, ICreateRatingCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateRatingValidator _validator;
        public EfCreateRatingCommand(BookingContext context, IApplicationActor actor, CreateRatingValidator validator)
            : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 41;

        public string Name => nameof(EfCreateRatingCommand);

        public void Execute(CreateRatingDto data)
        {
            _validator.ValidateAndThrow(data);
            var hasPastBooking = Context.Bookings.Any(x => x.UserId == _actor.Id && 
                                                      x.ApartmentId == data.ApartmentId && 
                                                      x.CheckOut < DateTime.UtcNow &&
                                                      x.IsActive);

            if (!hasPastBooking)
            {
                throw new PermissionDeniedException("You cannot leave a rating.");
            }

            var rating = new Rating
            {
                Date = DateTime.Now,
                ApartmentId = data.ApartmentId,
                Message = data.Comment,
                UserId = _actor.Id,

            };

            Context.Add(rating);

            var apartmentRatings = data.Values.Select(value => new ApartmentRating
            {
                StarRating = (int)value.Value,
                RatingTypeId = value.Id,
                Rating = rating

            });

            Context.AddRange(apartmentRatings);

            Context.SaveChanges();
        }
    }
}
