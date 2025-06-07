namespace API.Chat;

public interface IChatHub
{
    Task SendMessage(int receiverId, string message);
}
