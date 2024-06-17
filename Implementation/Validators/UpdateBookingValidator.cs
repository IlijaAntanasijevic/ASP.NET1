using Application.DTO.Bookings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class UpdateBookingValidator : AbstractValidator<EditBookingDto>
    {
        public UpdateBookingValidator()
        {
            
        }
    }
}
