using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OpenAiSetup : Entity
    {
        public string Model { get; set; }
        public string DefaultPromt { get; set; }

        public virtual ICollection<OpenAiConversation> Conversations { get; set; } = new HashSet<OpenAiConversation>();
    }
}
