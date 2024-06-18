using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BrsTgBot.Services.Interfaces;

public interface ITelegramService
{
    public Task SendTextMessageAsync(
        ChatId chatId,
        string text,
        CancellationToken cancellationToken,
        IReplyMarkup? replyMarkup = null,
        ParseMode? parseMode = null);

    public Task AnswerCallbackQueryAsync(
        CallbackQuery callbackQuery,
        CancellationToken cancellationToken,
        string text,
        int? cacheTime = null);
}