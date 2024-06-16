using BrsTgBot.Services.Factory;
using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class UpdateHandlers : IUpdateHandlers
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandlers> _logger;
    private readonly MessageUpdateHandler _messageUpdateHandler;
    private readonly CallbackQueryUpdateHandler _callbackQueryUpdateHandler;

    public UpdateHandlers(ITelegramBotClient botClient, ILogger<UpdateHandlers> logger,
        IUpdateHandlerFactory updateHandlerFactory)
    {
        _botClient = botClient;
        _logger = logger;
        _messageUpdateHandler = updateHandlerFactory.Create<MessageUpdateHandler>();
        _callbackQueryUpdateHandler = updateHandlerFactory.Create<CallbackQueryUpdateHandler>();
    }

    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received type update: {update.Type}");

        var handler = update.Type switch
        {
            UpdateType.Message => _messageUpdateHandler.HandleUpdateAsync(update, cancellationToken),
            UpdateType.CallbackQuery => _callbackQueryUpdateHandler.HandleUpdateAsync(update, cancellationToken)
        };
    }
}