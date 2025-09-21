using Application;
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
        private readonly IApplicationActor _actor;

        public EfOpenAIStartChatQuery(BookingContext context, OpenAISettings settings, IApplicationActor actor)
            : base(context)
        {
            _settings = settings;
            _actor = actor;
        }

        public int Id => 52;

        public string Name => nameof(EfOpenAIStartChatQuery);

        public OpenAIResponseDto Execute(OpenAIRequestDto search)
        {
            var openAiSettings = Context.OpenAiSetup.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.IsActive);
            ChatClient client = new ChatClient(model: openAiSettings.Model, apiKey: _settings.ApiKey);
            string actorName = _actor.Id == 0 ? "Unauthorized" : _actor.FirstName + " " + _actor.LastName;

            var city = Context.Cities.FirstOrDefault(x => x.Id == search.CityId);

            string prompt = " City: " + city.Name +
                      " Adults: " + search.Adults +
                      " Childrens: " + search.Childrens +
                     $" Date: {search.CheckIn.Date} – {search.CheckOut.Date}";


            ChatCompletion completion = client.CompleteChat(openAiSettings.DefaultPromt + prompt);
            string aiReply = completion.Content.FirstOrDefault()?.Text ?? "";

            var conv = new OpenAiConversation
            {
                Title = $"{actorName} {city.Name}",
                CreatedAt = DateTime.Now,
                SetupId = openAiSettings.Id
            };

            conv.Messages.Add(new OpenAiMessages 
            { 
                Sender = OpenAiSender.User, 
                Content = prompt, 
                CreatedAt = DateTime.Now,
                UserId = _actor.Id == 0 ? null : _actor.Id

            });
            conv.Messages.Add(new OpenAiMessages 
            { 
                Sender = OpenAiSender.AI, 
                Content = aiReply,
                CreatedAt = DateTime.Now,
                UserId = null
            });

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
