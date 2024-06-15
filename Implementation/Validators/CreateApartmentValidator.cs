using Application.DTO.Apartments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    //CHANGE DTO
    public class CreateApartmentValidator : AbstractValidator<ApartmentDto>
    {
        public CreateApartmentValidator()
        {
            
        }
    }
}
