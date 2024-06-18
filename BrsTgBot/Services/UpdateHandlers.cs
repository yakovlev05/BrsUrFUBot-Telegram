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

        if (!await EnsureAuthorizedInUrfuAsync(update, cancellationToken)) return; // Проверка на авторизацию в УрФУ


        var handler = update.Type switch
        {
            UpdateType.Message => messageUpdateHandler.HandleUpdateAsync(update, cancellationToken),
            UpdateType.CallbackQuery => callbackQueryUpdateHandler.HandleUpdateAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task<bool> IsAuthorizedInUrfuAsync(long chatId, CancellationToken cancellationToken)
    {
        //TODO: Сделать проверку на авторизацию в урфу
        return true;
    }

    private async Task<bool> EnsureAuthorizedInUrfuAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Type switch
        {
            UpdateType.Message => update.Message!.Chat.Id,
            UpdateType.CallbackQuery => update.CallbackQuery!.Message!.Chat.Id,
            _ => throw new ArgumentException($"Unsupported update type: {update.Type}")
        };
        if (await IsAuthorizedInUrfuAsync(chatId, cancellationToken)) return true;

        //TODO: Логика авторизации в урфу
        return false;
    }
}