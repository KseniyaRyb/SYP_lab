
namespace LR1
{
    public class ChatService : IChatService
    {

        private IBot _bot = null; //Ссылка на бота
        private readonly ICommandsRepository _commandRepoistory; //Класс-репозиторий с командами

        public ChatService(ICommandsRepository commandRepository)
        {
            this._commandRepoistory = commandRepository;
        }

        public void processMessage(ChatMessage chatMessage)
        {
            if (_bot != null)
            {
                //Логика поиска и обработки команд
                List<CommandEntity> commands = _commandRepoistory.FindByTrigger(chatMessage.Source);
                if (commands.Count == 0)
                {
                    _bot.SendMessageToChat(chatMessage.ChatId, "Неизвестная команда");
                }
                else
                { 
                    _bot.SendMessageToChat(chatMessage.ChatId, commands[0].CommandAnswer);
                }
            }
        }

        public void registerBot(IBot bot)
        {
            _bot = bot; //Регестрируем бота
        }
    }

}