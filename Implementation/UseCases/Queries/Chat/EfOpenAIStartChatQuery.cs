using Application.DTO;
using Application.UseCases.Queries.Chat;
using DataAccess;
using Domain;
using Implementation.Common;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat
{
    public class EfOpenAIStartChatQuery : EfUseCase, IOpenAIStartChatQuery
    {
        private readonly OpenAISettings _settings;

        public EfOpenAIStartChatQuery(BookingContext context, OpenAISettings settings)
            :base(context)
        {
            _settings = settings;
        }

        public int Id => 52;

        public string Name => nameof(EfOpenAIStartChatQuery);

        public OpenAIResponseDto Execute(OpenAIRequestDto search)
        {
            ChatClient client = new ChatClient(model: _settings.Model, apiKey: _settings.ApiKey);


            string prompt = " City: " + search.City +
                      " Country: " + search.Country +
                      " Adults: " + search.Adults +
                      " Childrens: " + search.Childrens +
                     $" Date: {search.CheckIn.Date} – {search.CheckOut.Date}";


            ChatCompletion completion = client.CompleteChat(_settings.Prompt + prompt);
            string aiReply = completion.Content.FirstOrDefault()?.Text ?? "";

            var conv = new OpenAiConversation
            {
                Title = $"{search.City}, {search.Country}",
                CreatedAt = DateTime.Now
            };

            conv.Messages.Add(new OpenAiMessages { Sender = OpenAiSender.User, Content = prompt, CreatedAt = DateTime.Now });
            conv.Messages.Add(new OpenAiMessages { Sender = OpenAiSender.AI, Content = aiReply, CreatedAt = DateTime.Now });

            Context.OpenAiConversation.Add(conv);
            Context.SaveChanges();

            return new OpenAIResponseDto
            {
                Text = aiReply,
                ConversationId = conv.Id
            };
        }
    }
}
