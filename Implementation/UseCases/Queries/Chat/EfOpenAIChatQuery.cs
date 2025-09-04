using Application.DTO;
using Application.UseCases.Queries.Chat;
using Implementation.Common;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat
{
    public class EfOpenAIChatQuery : IOpenAIChatQuery
    {
        private readonly OpenAISettings _settings;

        public EfOpenAIChatQuery(OpenAISettings settings)
        {
            _settings = settings;
        }

        public int Id => 52;

        public string Name => nameof(EfOpenAIChatQuery);

        public OpenAIResponseDto Execute(OpenAIRequestDto search)
        {
            ChatClient client = new ChatClient(model: "gpt-3.5-turbo", apiKey: _settings.ApiKey);


            string city = "Grad: " + search.City;
            string country = "Drzava: " + search.Country;
            string adults = "Odrasli: " + search.Adults;
            string children = "Deca: " + search.Childrens;
            string date = $"Datum: {search.CheckIn.Date} – {search.CheckOut.Date}";

            ChatCompletion completion = client.CompleteChat(_settings.PromptSrb + $"{city} {country} {adults} {children} {date}");
            string text = completion.Content.FirstOrDefault()?.Text ?? "";
            Console.WriteLine(text);

            return new OpenAIResponseDto
            {
                Text = text,
            };
        }
    }
}
