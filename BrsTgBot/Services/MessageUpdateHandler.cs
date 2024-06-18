using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BrsTgBot.Services;

public class MessageUpdateHandler(
    ILogger<MessageUpdateHandler> logger,
    ITelegramService telegramService) : IUpdateHandler<MessageUpdateHandler>
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        if (update.Message is null) return;
        logger.LogInformation("Receive message type: {MessageType}", update.Message.Type);

        var messageText = update.Message.Text;

        var action = messageText switch
        {
            "/start" => SendMainMenuAsync(update.Message, cancellationToken),
        };

        await action;
    }

    private async Task SendMainMenuAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending main menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("БРС 📊", "brs"),
                InlineKeyboardButton.WithCallbackData("Настройки ⚙️", "settings")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl("О проекте", "https://github.com/yakovlev05/BrsUrFUBot-Telegram")
            }
        });

        await telegramService.SendTextMessageAsync(message.Chat.Id,
            $@"Добро пожаловать, *{message.Chat.FirstName}*\! Используйте кнопки для навигации\.",
            cancellationToken,
            keyboard,
            ParseMode.MarkdownV2);
    }
}