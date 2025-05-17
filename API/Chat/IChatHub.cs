namespace API.Chat;

public interface IChatHub
{
    Task SendMessage(int senderId, int receiverId, string message);
}
