using Application.DTO.Admin;
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
    public class EfUpdateOpenAiSetupCommand : EfUseCase, IUpdateOpenAiSetupCommand
    {
        public EfUpdateOpenAiSetupCommand(BookingContext context) : base(context)
        {
        }

        public int Id => 71;

        public string Name => nameof(EfUpdateOpenAiSetupCommand);

        public void Execute(OpenAiSetupDataDto data)
        {
            var disableCurrent = Context.OpenAiSetup.FirstOrDefault(x => x.IsActive);
            if (disableCurrent != null)
            {
                disableCurrent.IsActive = false;
                disableCurrent.UpdatedAt = DateTime.Now;
            }

            var newSetup = new OpenAiSetup
            {
                Model = data.Model,
                DefaultPromt = data.Prompt,
                IsActive = true
            };

            Context.Add(newSetup);
            Context.SaveChanges();
        }
    }
}
