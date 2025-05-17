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
