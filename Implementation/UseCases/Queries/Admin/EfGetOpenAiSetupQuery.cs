using Application.DTO.Admin;
using Application.UseCases.Queries.Admin;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetOpenAiSetupQuery : EfUseCase, IGetOpenAiSetupQuery
    {
        public EfGetOpenAiSetupQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 70;

        public string Name => nameof(EfGetOpenAiSetupQuery);

        public OpenAiSetupDto Execute(int search)
        {
            var setup = Context.OpenAiSetup.ToList();
            var current = setup.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.IsActive);
            var previous = setup.OrderByDescending(x => x.CreatedAt).Where(x => !x.IsActive).ToList();

            var response = new OpenAiSetupDto
            {
                CurrentActive = new OpenAiSetupDataDto
                {
                    Id = current.Id,
                    Prompt = current.DefaultPromt,
                    CreatedAt = current.CreatedAt,
                    UpdatedAt = current.UpdatedAt,
                    Model = current.Model
                },
                PreviousConf = previous.Select(x => new OpenAiSetupDataDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Model = x.Model,
                    Prompt = x.DefaultPromt
                }).ToList()
            };

            return response;
        }
    }
}
