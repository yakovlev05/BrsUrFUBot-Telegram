using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class UpdateHandlers(ITelegramBotClient botClient, ILogger<UpdateHandlers> logger) : IUpdateHandlers
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly ILogger<UpdateHandlers> _logger = logger;

    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received type update: {update.Type}");

        var handler = update.Type switch
        {
            UpdateType.Message => new Task(() => Console.WriteLine("ТЕКСТ"))
        };
        
        handler.Start();
        await handler;
    }
}