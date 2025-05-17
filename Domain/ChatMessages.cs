using App.Domain;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ChatMessages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool Seen { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
