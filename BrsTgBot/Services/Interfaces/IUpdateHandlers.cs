using Telegram.Bot.Types;

namespace BrsTgBot.Services.Interfaces;

public interface IUpdateHandlers
{
    public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken);
}