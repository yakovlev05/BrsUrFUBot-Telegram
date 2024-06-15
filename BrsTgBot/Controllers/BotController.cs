using BrsTgBot.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BrsTgBot.Controllers;

[ApiController]
[Route("/")]
public class BotController : Controller
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUpdateHandlers _updateHandlers;

    public BotController(ITelegramBotClient botClient, IUpdateHandlers updateHandlers)
    {
        _botClient = botClient;
        _updateHandlers = updateHandlers;
    }

    [HttpPost]
    public async void Post(Update update, CancellationToken cancellationToken)
    {
        await _updateHandlers.HandleUpdateAsync(update, cancellationToken);
    }

    public ActionResult<string> Check()
    {
        return "Соединение есть";
    }
}