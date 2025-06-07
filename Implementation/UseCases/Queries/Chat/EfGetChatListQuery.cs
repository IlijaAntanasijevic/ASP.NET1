using Application;
using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Queries.Chat;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat;

public class EfGetChatListQuery : EfUseCase, IGetChatListQuery
{
    private readonly IApplicationActor _currentUser;
    public EfGetChatListQuery(BookingContext context, IApplicationActor currentUser)
        : base(context)
    {
        _currentUser = currentUser;
    }

    public int Id => 35;

    public string Name => nameof(EfGetChatListQuery);

    public List<ChatListDto> Execute(BasicSearch search)
    {
        var chatList = Context.ChatMessages.Include(x => x.Receiver)
                                           .Include(x => x.Sender)
                                           .Where(x => x.SenderId == _currentUser.Id || x.ReceiverId == _currentUser.Id)
                                           //.GroupBy(x => x.ReceiverId)
                                           .GroupBy(x => x.SenderId == _currentUser.Id ? x.ReceiverId : x.SenderId)
                                           .Select(group => group.OrderByDescending(x => x.ReceivedDate).FirstOrDefault()).ToList();

        var response = chatList.Select(x => new ChatListDto
        {
            Id = x.Id,
            ReceiverId = _currentUser.Id == x.ReceiverId ? x.SenderId : x.ReceiverId,
            FullName =  _currentUser.Id == x.ReceiverId ? x.Sender.FirstName + " " + x.Sender.LastName : x.Receiver.FirstName + " " + x.Receiver.LastName,
            LastChatMessage = x.ReceivedDate,
            IsRead = x.Seen
        }).ToList();

        return response;
    }
}
