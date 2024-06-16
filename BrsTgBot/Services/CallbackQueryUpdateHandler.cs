using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;

namespace BrsTgBot.Services;

public class CallbackQueryUpdateHandler : IUpdateHandler
{
    public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}