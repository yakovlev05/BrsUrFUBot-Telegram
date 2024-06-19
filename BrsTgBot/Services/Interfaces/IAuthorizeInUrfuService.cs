using Telegram.Bot.Types;

namespace BrsTgBot.Services.Interfaces;

public interface IAuthorizeInUrfuService
{
    Task<bool> EnsureAuthorizedInUrfuAsync(Update update, CancellationToken cancellationToken);
}