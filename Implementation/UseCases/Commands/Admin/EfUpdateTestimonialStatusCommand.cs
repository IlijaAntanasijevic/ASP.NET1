using Application.Exceptions;
using Application.UseCases.Commands.Admin;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Admin
{
    public class EfUpdateTestimonialStatusCommand : EfUseCase, IUpdateTestimonialStatusCommand
    {
        public EfUpdateTestimonialStatusCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 68;

        public string Name => nameof(EfUpdateTestimonialStatusCommand);

        public void Execute(int id)
        {
            var testimonial = Context.Ratings.FirstOrDefault(x => x.Id == id);
            if (testimonial == null)
            {
                throw new EntityNotFoundException(nameof(Rating), id);
            }

            testimonial.IsVisibleOnHome = !testimonial.IsVisibleOnHome;

            Context.SaveChanges();
        }
    }
}
