using System;
using System.Threading.Tasks;

namespace LR1
{
    public interface IBot
    {
        Task SendMessageToChat(long chatId, String text);
    }
}