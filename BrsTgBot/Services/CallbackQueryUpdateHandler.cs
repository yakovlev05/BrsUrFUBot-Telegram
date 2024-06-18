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
        logger.LogInformation("Callback data received: {data}", callbackQuery.Data);

        var action = callbackQuery.Data switch
        {
            "brs" => AnswerForBrsAsync(callbackQuery, cancellationToken),
            "settings" => Task.CompletedTask,
            _ => Task.CompletedTask
        };

        await action;
    }

    private async Task AnswerForBrsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending answer for callback query: {data}", callbackQuery.Data);

        await telegramService.AnswerCallbackQueryAsync(
            callbackQuery,
            cancellationToken,
            "Тест ответа на брс");
    }
}