using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;

namespace BrsTgBot.Services;

public class CallbackQueryUpdateHandler(ILogger<CallbackQueryUpdateHandler> logger, ITelegramService telegramService)
    : IUpdateHandler<CallbackQueryUpdateHandler>
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery;
        if (callbackQuery is null) return;

        var message = callbackQuery.Message;
        if (message is null) return;

        logger.LogInformation("Callback data received: {data}", callbackQuery.Data);

        var action = callbackQuery.Data switch
        {
            "brs_menu" => telegramService.SendBrsMenuAsync(message, cancellationToken),
            "settings_menu" => telegramService.SendSettingsMenuAsync(message, cancellationToken),
            "main_menu" => telegramService.SendMainMenuAsync(message, cancellationToken),
            "brs_update" => telegramService.SendBrsUpdateAsync(message, cancellationToken),
            "settings_update_auth" => telegramService.SendSettingsUpdateAuthAsync(message, cancellationToken),
            "settings_remove_account" => telegramService.SendSettingsDeleteAccountQuestionAsync(message,
                cancellationToken),
            "settings_update_auth_cancel" => telegramService.SendSettingsMenuAsync(message, cancellationToken),
            "settings_delete_account_question_cancel" => telegramService.SendSettingsMenuAsync(message, cancellationToken),
            _ => Task.CompletedTask
        };

        await action;
    }
}