using Telegram.Bot.Types;

namespace BrsTgBot.Services.Interfaces;

public interface ITelegramService
{
    public Task SendNewMainMenuAsync(Message message, CancellationToken cancellationToken);
    public Task SendBrsMenuAsync(Message message, CancellationToken cancellationToken);
    public Task SendSettingsMenuAsync(Message message, CancellationToken cancellationToken);
    public Task SendMainMenuAsync(Message message, CancellationToken cancellationToken);
    public Task SendBrsUpdateAsync(Message message, CancellationToken cancellationToken);
    public Task SendSettingsUpdateAuthAsync(Message message, CancellationToken cancellationToken);
    public Task SendSettingsDeleteAccountQuestionAsync(Message message, CancellationToken cancellationToken);
    public Task SendErrorMessageAsync(long chatId, string text, CancellationToken cancellationToken);
}