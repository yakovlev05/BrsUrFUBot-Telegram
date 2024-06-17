using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BrsTgBot.Services;

public class MessageUpdateHandler(
    ITelegramBotClient botClient,
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
            "/start" => botClient.SendTextMessageAsync(update.Message.Chat.Id, "start",
                cancellationToken: cancellationToken)
        };

        await action;
    }
}