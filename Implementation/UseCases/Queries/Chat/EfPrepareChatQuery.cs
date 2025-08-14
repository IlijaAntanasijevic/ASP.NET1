using App.Domain;
using Application;
using Application.DTO;
using Application.DTO.Search;
using Application.Exceptions;
using Application.UseCases.Queries.Chat;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Chat
{
    public class EfPrepareChatQuery : EfUseCase, IPrepareChatQuery
    {
        private readonly IApplicationActor _actor;
        public EfPrepareChatQuery(BookingContext context, IApplicationActor actor)
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 38;

        public string Name => nameof(EfPrepareChatQuery);

        public PrepareChatDto Execute(int receiverId)
        {
            var messages = Context.ChatMessages.Include(x => x.Receiver).Include(x => x.Sender)
                             .Where(x => (x.SenderId == _actor.Id && x.ReceiverId == receiverId) ||
                                   (x.SenderId == receiverId && x.ReceiverId == _actor.Id))
                             .OrderBy(x => x.ReceivedDate)
                             .ToList();

            if (messages.Any())
            {
                var latestMessage = messages.Last();

                return new PrepareChatDto
                {
                    ChatInfo = new ChatListDto
                    {
                        Id = latestMessage.Id,
                        ReceiverId = _actor.Id == latestMessage.ReceiverId ? latestMessage.SenderId : latestMessage.ReceiverId,
                        FullName = _actor.Id == latestMessage.ReceiverId
                      ? $"{latestMessage.Sender.FirstName} {latestMessage.Sender.LastName}"
                      : $"{latestMessage.Receiver.FirstName} {latestMessage.Receiver.LastName}",
                        LastChatMessage = latestMessage.ReceivedDate,
                        IsRead = latestMessage.Seen
                    },
                    Messages = messages.Select(x => new ChatMessagesDto
                    {
                        Id = x.Id,
                        SenderId = x.SenderId,
                        ReceiverId = x.ReceiverId,
                        Message = x.Message,
                        SentAt = x.ReceivedDate,
                        isMineMessage = x.SenderId == _actor.Id,
                        IsRead = x.Seen
                    }).ToList()
                };
            }

            var receiver = Context.Users.FirstOrDefault(u => u.Id == receiverId);
            if (receiver == null) throw new EntityNotFoundException(nameof(User), receiverId);


            return new PrepareChatDto
            {
                ChatInfo = new ChatListDto
                {
                    Id = 0,
                    ReceiverId = receiver.Id,
                    FullName = $"{receiver.FirstName} {receiver.LastName}",
                    LastChatMessage = null,
                    IsRead = true
                },
                Messages = new List<ChatMessagesDto>()
            };


        }
    }
}
