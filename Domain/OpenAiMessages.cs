using App.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OpenAiMessages
    {
        public int Id {  get; set; }
        public int ConversationId { get; set; }
        public OpenAiSender Sender { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual OpenAiConversation Conversation { get; set; }
        public virtual User User { get; set; }
    }

    public enum OpenAiSender
    {
        User = 1,
        AI = 2
    }
}
