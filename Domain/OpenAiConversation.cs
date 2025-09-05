using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OpenAiConversation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<OpenAiMessages> Messages { get; set; } = new HashSet<OpenAiMessages>();
    }
}
