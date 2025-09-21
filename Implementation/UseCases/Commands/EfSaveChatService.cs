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

public class EfSaveChatService : EfUseCase, ISendMessageService
{
    public EfSaveChatService(BookingContext context) : base(context)
    {
    }

    public int Id => 34;

    public string Name => nameof(EfSaveChatService);

    public void SendMessage(ChatDto data)
    {
        var user = Context.UserUseCases.Where(x => x.UserId == data.SenderId).ToList();
        var canSendMessage = user.Select(x => x.UseCaseId).Contains(this.Id);

        if(user == null || !user.Any() || !canSendMessage)
        {
            throw new UnauthorizedAccessException();
        }

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
