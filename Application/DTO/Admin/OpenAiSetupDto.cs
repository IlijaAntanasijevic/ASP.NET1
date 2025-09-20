using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class OpenAiSetupDto
    {
        public OpenAiSetupDataDto CurrentActive { get; set; }
        public List<OpenAiSetupDataDto> PreviousConf { get; set; }
    }

    public class OpenAiSetupDataDto
    {
        public int? Id { get; set; }
        public string Prompt { get; set; }
        public string Model { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class OpenAiConversationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Model { get; set; }
        public List<OpenAiMessageDto> Messages { get; set; } = new();
    }

    public class OpenAiMessageDto
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; } 
        public string Content { get; set; }
        public string Sender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
