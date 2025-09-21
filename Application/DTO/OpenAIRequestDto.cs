using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class OpenAIRequestDto
    {
        public int Adults { get; set; } = 1;
        public int Childrens { get; set; } = 0;
        public int CityId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    public class OpenAIResponseDto
    {
        public int? ConversationId { get; set; }
        public string Text { get; set; }
    }

    public class OpenAIConituneConversationDto 
    {
        public int ConversationId { get; set; }
        public string Text { get; set; }
    }
}
