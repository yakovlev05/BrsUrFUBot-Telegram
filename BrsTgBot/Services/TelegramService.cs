using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BrsTgBot.Services;

public class TelegramService(ILogger<TelegramService> logger, ITelegramBotClient botClient) : ITelegramService
{
    public async Task SendTextMessageAsync(ChatId chatId, string text, CancellationToken cancellationToken,
        IReplyMarkup? replyMarkup = null, ParseMode? parseMode = null)
    {
        logger.LogInformation("Sending text message: {message}", text);

        await botClient.SendTextMessageAsync(
            chatId,
            text,
            cancellationToken: cancellationToken,
            replyMarkup: replyMarkup,
            parseMode: parseMode);
    }

    public async Task AnswerCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken,
        string text, int? cacheTime = null)
    {
        logger.LogInformation("Answering on callback query. Data: {data}", callbackQuery.Data);

        await botClient.AnswerCallbackQueryAsync(
            callbackQuery.Id,
            text,
            cancellationToken: cancellationToken,
            cacheTime: cacheTime);
    }
}