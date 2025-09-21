using Application.DTO;
using Application.DTO.Users;
using Application.UseCases.Queries.Chat;
using Azure.Core;
using DataAccess;
using Domain;
using Implementation.Common;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat
{
    public class EfOpenAIMessageQuery : EfUseCase, IOpenAIMessageQuery
    {
        private readonly OpenAISettings _settings;
        public EfOpenAIMessageQuery(BookingContext context, OpenAISettings settings) : base(context)
        {
            _settings = settings;
        }

        public int Id => 53;

        public string Name => nameof(EfOpenAIMessageQuery);

        public OpenAIResponseDto Execute(OpenAIConituneConversationDto data)
        {
            return ExecuteInternal(data).GetAwaiter().GetResult();
        }

        private async Task<OpenAIResponseDto> ExecuteInternal(OpenAIConituneConversationDto request)
        {
            var openAiSettings = Context.OpenAiSetup.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.IsActive);
            ChatClient client = new ChatClient(model: openAiSettings.Model, apiKey: _settings.ApiKey);
            var conversation = Context.OpenAiConversation.Include(x => x.Messages).FirstOrDefault(x => x.Id == request.ConversationId);

            if (conversation == null)
            {
                var completionDef = await client.CompleteChatAsync(openAiSettings.DefaultPromt + "You cant find history about chat.");
                var reply = completionDef.Value.Content.FirstOrDefault()?.Text ?? "";

                return new OpenAIResponseDto
                {
                    ConversationId = null,
                    Text = reply
                };
            }

            var history = conversation.Messages.Select(x => x.Sender == OpenAiSender.User ? (ChatMessage)new UserChatMessage(x.Content) : new AssistantChatMessage(x.Content)).ToList();

            history.Add(new UserChatMessage(request.Text));

            var completion = await client.CompleteChatAsync(history);

            string assistantReply = completion.Value.Content.FirstOrDefault()?.Text ?? "";

            conversation.Messages.Add(new OpenAiMessages
            {
                Content = request.Text,
                CreatedAt = DateTime.Now,
                Sender = OpenAiSender.User
            });

            conversation.Messages.Add(new OpenAiMessages
            {
                Content = assistantReply,
                CreatedAt = DateTime.Now,
                Sender = OpenAiSender.AI
            });

            Context.SaveChanges();

            return new OpenAIResponseDto
            {
                ConversationId = conversation.Id,
                Text = assistantReply
            };
        }
    }
}
