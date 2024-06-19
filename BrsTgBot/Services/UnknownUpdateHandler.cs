using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;

namespace BrsTgBot.Services;

public class UnknownUpdateHandler(ILogger<UnknownUpdateHandler> logger, ITelegramService telegramService)
    : IUpdateHandler<UnknownUpdateHandler>
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown update handler: {type}", update.Type);

        await Task.CompletedTask;
    }
}