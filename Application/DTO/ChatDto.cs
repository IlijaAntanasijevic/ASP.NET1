using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO;

public class ChatDto
{
    public string Message { get; set; }
    public DateTime ReceivedDate { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}

public class ChatListDto
{
    public int Id { get; set; }
    public int ReceiverId { get; set; }
    public string FullName { get; set; }
    public DateTime LastChatMessage { get; set; }
    public bool IsRead { get; set; }
}

public class ChatMessagesDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    //public string SenderFullName { get; set; }
    //public string ReceiverFullName { get; set; }
    public bool IsRead { get; set; }
    public bool isMineMessage { get; set; } //Flag za front
}
