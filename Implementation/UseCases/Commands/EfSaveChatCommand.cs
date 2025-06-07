using Application.DTO;
using Application.UseCases.Commands;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands;

public class EfSaveChatCommand : EfUseCase, ISendMessageCommand
{
    public EfSaveChatCommand(BookingContext context) : base(context)
    {
    }

    public int Id => 34;

    public string Name => nameof(EfSaveChatCommand);

    public void Execute(ChatDto data)
    {
        var chatMessage = new ChatMessages
        {
            Message = data.Message,
            ReceivedDate = data.ReceivedDate,
            SenderId = data.SenderId,
            ReceiverId = data.ReceiverId
        };

        Context.ChatMessages.Add(chatMessage);
        Context.SaveChanges();
    }
}
