using Application;
using Application.DTO;
using Application.UseCases.Commands;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Chat;

public sealed class ChatHub : Hub, IChatHub
{
    private readonly UseCaseHandler _handler;
    private readonly ISendMessageService _sendMessageService;

    public ChatHub(UseCaseHandler handler, ISendMessageService sendMessageService)
    {
        _handler = handler;
        _sendMessageService = sendMessageService;
    }

    public override Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.UserIdentifier;
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(int receiverId, string message)
    {
        var tmp = Context.ConnectionId;
        var tmp2 = Clients.User;
        var tmp3 = Context.UserIdentifier;
        if (!int.TryParse(Context.UserIdentifier, out var senderId)) throw new HubException("sender id is invalid");

        var dataForDb = new ChatDto
        {
            Message = message,
            SenderId = senderId,
            ReceiverId = receiverId,
            ReceivedDate = DateTime.Now,
        };


        try
        {
            _sendMessageService.SendMessage(dataForDb);
        }
        catch (Exception ex)
        {

            throw;
        }

        var dataForFront = new ChatMessagesDto
        {
            Message = message,
            SenderId = senderId,
            ReceiverId = receiverId,
            SentAt = DateTime.Now,
            IsRead = false,
            isMineMessage = false
        };

        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", dataForFront);

        dataForFront.isMineMessage = true;
        await Clients.User(senderId.ToString()).SendAsync("SendMessage", dataForFront);
    }

}
