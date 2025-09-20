using Application.DTO.Admin;
using Application.UseCases.Queries.Admin;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetOpenAiConversationsQuery : EfUseCase, IGetOpenAiConversationsQuery
    {
        public EfGetOpenAiConversationsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 72;

        public string Name => nameof(EfGetOpenAiConversationsQuery);

        public IEnumerable<OpenAiConversationDto> Execute(int search)
        {
            var conversations = Context.OpenAiConversation
                .Include(x => x.Setup)
                .Include(x => x.Messages)
                .ThenInclude(x => x.User)
                .OrderByDescending(x => x.CreatedAt).ToList();

            var response = conversations.Select(x => new OpenAiConversationDto
            {
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt,    
                Model = x.Setup.Model,
                Messages = x.Messages.Select(m => new OpenAiMessageDto
                {
                    Id = m.Id,
                    ConversationId = m.ConversationId,
                    UserId = m.UserId,
                    UserName = m.User != null ? m.User.FirstName + " " + m.User.LastName : "Unathorized",
                    Content = m.Content,
                    CreatedAt = m.CreatedAt,
                    Sender = m.Sender.ToString(),
                }).ToList()
            });

            return response;
        }
    }
}
