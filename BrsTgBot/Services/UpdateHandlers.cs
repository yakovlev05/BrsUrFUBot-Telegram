using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class UpdateHandlers(
    ITelegramBotClient botClient,
    ILogger<UpdateHandlers> logger,
    IUpdateHandler<MessageUpdateHandler> messageUpdateHandler,
    IUpdateHandler<CallbackQueryUpdateHandler> callbackQueryUpdateHandler)
    : IUpdateHandlers
{
    private readonly ITelegramBotClient _botClient = botClient;

    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Received type update: {update.Type}");

        var handler = update.Type switch
        {
            UpdateType.Message => messageUpdateHandler.HandleUpdateAsync(update, cancellationToken),
            UpdateType.CallbackQuery => callbackQueryUpdateHandler.HandleUpdateAsync(update, cancellationToken)
        };
    }
}