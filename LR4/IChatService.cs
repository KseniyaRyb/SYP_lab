
namespace LR1
{
    public interface IChatService
    {
        void processMessage(ChatMessage chatMessage);
        void registerBot(IBot bot);
    }
}