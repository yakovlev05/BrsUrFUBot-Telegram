using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class UpdateHandlers(
    ILogger<UpdateHandlers> logger,
    IUpdateHandler<MessageUpdateHandler> messageUpdateHandler,
    IUpdateHandler<CallbackQueryUpdateHandler> callbackQueryUpdateHandler,
    IUpdateHandler<UnknownUpdateHandler> unknownUpdateHandler,
    IAuthorizeInUrfuService authorizeInUrfuService)
    : IUpdateHandlers
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Received type update: {update.Type}");

        if (!await authorizeInUrfuService.EnsureAuthorizedInUrfuAsync(update, cancellationToken))
            return; // Проверка на авторизацию в УрФУ


        var handler = update.Type switch
        {
            UpdateType.Message => messageUpdateHandler.HandleUpdateAsync(update, cancellationToken),
            UpdateType.CallbackQuery => callbackQueryUpdateHandler.HandleUpdateAsync(update, cancellationToken),
            _ => unknownUpdateHandler.HandleUpdateAsync(update, cancellationToken)
        };

        await handler;
    }
}