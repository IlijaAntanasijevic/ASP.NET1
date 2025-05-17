using Application.DTO;
using Application.UseCases.Commands;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Chat;

public sealed class ChatHub : Hub, IChatHub
{
    private readonly UseCaseHandler _handler;
    private readonly ISaveChatCommand _command;

    public ChatHub(UseCaseHandler handler, ISaveChatCommand command)
    {
        _handler = handler;
        _command = command;
    }

    public async Task SendMessage(int senderId, int receiverId, string message)
    {
        var data = new ChatDto
        {
            Message = message,
            SenderId = senderId,
            ReceiverId = receiverId,
            ReceivedDate = DateTime.Now,
        };

        _handler.HandleCommand(_command, data);

        await Clients.User(receiverId.ToString()).SendAsync("ReciveMessage", senderId, message);
    }

}
