using BrsTgBot.HttpClients.UserClient.Abstract;
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
    private readonly IUserClient _userClient;

    public BotController(ITelegramBotClient botClient, IUpdateHandlers updateHandlers, IUserClient userClient)
    {
        _botClient = botClient;
        _updateHandlers = updateHandlers;
        _userClient = userClient;
    }
}