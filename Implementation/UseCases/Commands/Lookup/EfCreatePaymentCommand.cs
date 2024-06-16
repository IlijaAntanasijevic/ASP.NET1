using Application.DTO;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using Domain.Lookup;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup
{
    public class EfCreatePaymentCommand : EfUseCase, ICreatePaymentCommand
    {
        private readonly CreatePaymentValidator _validator;
        public EfCreatePaymentCommand(BookingContext context, CreatePaymentValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => nameof(EfCreatePaymentCommand);

        public void Execute(NamedDto data)
        {
            _validator.ValidateAndThrow(data);

            var payment = new Payment { Name = data.Name };
            Context.Payments.Add(payment);
            Context.SaveChanges();
        }
    }
}
