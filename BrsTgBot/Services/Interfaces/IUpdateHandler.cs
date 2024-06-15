using Telegram.Bot.Types;

namespace BrsTgBot.Services.Interfaces;

public interface IUpdateHandler
{
    public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken);
}