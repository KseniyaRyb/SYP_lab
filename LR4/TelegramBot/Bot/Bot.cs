using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using LR1;
using LR1.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LR1
{
    public class Bot : IBot, IHostedService
    {

        private IChatService _chatService;
        private readonly ILogger<Bot> _logger;//Логгер для красивого логирования

        private readonly TelegramBotClient _botClient = new TelegramBotClient("токен"); //Создание ТГ-бота - нужно указать токен
        
        private readonly IServiceScopeFactory _services;
        
        public Bot(IServiceScopeFactory services, ILogger<Bot> logger)
        {
            this._logger = logger;
            this._services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { //Запускается при запуске IHostedService, при регистрации
            using var cts = new CancellationTokenSource();

            var scope = _services.CreateScope(); //Создаем сокуп

            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>(); //Получаем из скоупа контекст для БД
            var repository = new CommandRepository(dbContext); // Создаем репозиторий на основе контекста
            var chatService = new ChatService(repository); // Создаем сервис команд на основе репозитория
            chatService.registerBot(this);

            _chatService = chatService;

            _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, null, cts.Token); //Начинаем получать и обрабатывать сообщения/обновления в методах HandleUpdateAsync и HandleErrorAsync

            _logger.LogInformation("Bot init"); //Логируем 
            return Task.CompletedTask;
            

            

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SendMessageToChat(long chatId, String text)
        {// Метод для отправки сообщения в чат
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text
            ); //Асинхронная отправка сообщения с укзанием ИД чата и самим текстом
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        { //Поимка ошибок API и вывод их в лог
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            }; // Распаковываем содержимое ошибки, получая сообщение ошибки

            _logger.LogError("Error on Telegram Api", exception); //Логируем ошибку
            return Task.CompletedTask; // Завершаем поток
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
            { //Если полученное изменение в чате не сообщение - пропускаем
                return;
            }
            if (update.Message.Type != MessageType.Text)
            {//Если это не текст - пропускаем
                return;
            }

            var chatId = update.Message.Chat.Id; //Получаем ИД чата
            _logger.LogInformation($"Получено '{update.Message.Text}' в чате {chatId}."); // Для удобства логируем пришедшее сообщение
                                                                                          //string sss = _Controller.Get(update.Message.Text).ToString();
            // await SendMessageToChat(chatId, update.Message.Text); //Отправляем ответ
                                                                  // ChatMessage message = new ChatMessage("telegram", chatId, update.Message.Text);
                                                                  //await _producer.ProduceAsync("receiveMessage", new Message<Null, string> { Value = JsonConvert.SerializeObject(message) });
            _chatService.processMessage(new ChatMessage(update.Message.Text, chatId, "Отправить"));
        }

        /* Task SendMessageToChat(ChatMessage chatMessage)
        {
            throw new NotImplementedException();
        }*/
    }
}
