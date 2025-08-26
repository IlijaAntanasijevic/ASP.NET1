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
    public class EfChangePasswordCommand : EfUseCase, IChangePasswordCommand
    {
        public EfChangePasswordCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 50;

        public string Name => nameof(EfChangePasswordCommand);

        public void Execute(EmailCodeDto data)
        {
            throw new NotImplementedException();
        }
    }
}
