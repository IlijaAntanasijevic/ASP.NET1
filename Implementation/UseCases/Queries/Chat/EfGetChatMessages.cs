using Application;
using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Chat;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat;

public class EfGetChatMessages : EfUseCase, IGetChatMessagesQuery
{
    private readonly IApplicationActor _currentUser;
    public EfGetChatMessages(BookingContext context, IApplicationActor currentUser) : base(context)
    {
        _currentUser = currentUser;
    }

    public int Id => 36;

    public string Name =>nameof(EfGetChatMessages);

    public List<ChatMessagesDto> Execute(int receiverId)
    {
        var messages = Context.ChatMessages.Where(x => (x.SenderId == _currentUser.Id && x.ReceiverId == receiverId) ||
                                                       (x.SenderId == receiverId && x.ReceiverId == _currentUser.Id))
                                            .OrderBy(x => x.ReceivedDate).ToList();

        var response = messages.Select(x => new ChatMessagesDto
        {
            Id = x.Id,
            Message = x.Message,
            SentAt = x.ReceivedDate,
            SenderId = x.SenderId,
            ReceiverId = x.ReceiverId,
            IsRead = x.Seen,
            isMineMessage = x.SenderId == _currentUser.Id
        }).ToList();

        return response;
    }
}
