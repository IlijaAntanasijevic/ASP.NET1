using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands.Lookup;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Lookup
{
    public class EfUpdatePaymentMethodCommand : EfUseCase, IUpdatePaymentMethodCommand
    {
        public EfUpdatePaymentMethodCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 61;

        public string Name => nameof(EfUpdatePaymentMethodCommand);

        public void Execute(PaymentMethodsDto data)
        {
            var payment = Context.Payments.FirstOrDefault(x => x.Id == data.Id);
            if(payment == null)
            {
                throw new EntityNotFoundException(nameof(EfUpdatePaymentMethodCommand), (int)data.Id);
            }

            payment.Name = data.Name;
            payment.ProcessingFee = data.ProcessingFee;
            payment.IsActive = data.IsActive ?? true;
            payment.Icon = data.Icon;

            Context.SaveChanges();
        }
    }
}
