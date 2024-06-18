using BrsTgBot.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BrsTgBot.Controllers;

[ApiController]
[Route("/")]
public class BotController(IUpdateHandlers updateHandlers) : Controller
{
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
    {
        await updateHandlers.HandleUpdateAsync(update, cancellationToken);
        return Ok();
    }
}