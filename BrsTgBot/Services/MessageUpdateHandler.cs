using BrsTgBot.Services.Interfaces;
using Telegram.Bot.Types;

namespace BrsTgBot.Services;

public class MessageUpdateHandler : IUpdateHandlers
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine("ТЕКСТ");
    }
}