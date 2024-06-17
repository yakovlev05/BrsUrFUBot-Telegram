using Telegram.Bot.Types;

namespace BrsTgBot.Services.Interfaces;

public interface IUpdateHandler<T> where T : class
{
    public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken);
}