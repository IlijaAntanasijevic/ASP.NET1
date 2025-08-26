using Application.DTO.Users;
using Application.UseCases.Commands.Users;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users
{
    public class EfForgotPasswordSendEmailCommand : EfUseCase, IForgotPasswordSendEmailCommand
    {
        public EfForgotPasswordSendEmailCommand(BookingContext context) 
            : base(context)
        {
        }

        public int Id => 48;

        public string Name => nameof(EfForgotPasswordSendEmailCommand);

        public void Execute(EmailCodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
