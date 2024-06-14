using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BrsTgBot.Controllers;

[ApiController]
[Route("/")]
public class BotController : Controller
{
    private readonly ITelegramBotClient _botClient;

    public BotController(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    [HttpPost]
    public void Post(Update update)
    {
        Console.WriteLine(update.Message.Text);
        _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Hello, world!");
    }
}