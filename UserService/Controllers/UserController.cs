using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserService.Data.Entities;
using UserService.Data.Repositories;
using UserService.Models.UserController;

namespace UserService.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IRepository<UserEntity> repository) : Controller
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserRequestModel request)
    {
        var users = await repository.GetAllAsync();
        if (users.Any(x => x.TelegramChatId == request.TelegramChatId))
            return BadRequest("User already exist");

        var newUser = new UserEntity()
        {
            TelegramChatId = request.TelegramChatId,
            TelegramUsername = request.TelegramUsername
        };

        await repository.AddAsync(newUser);
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("chatId/{id}")]
    public async Task<ActionResult> AuthenticateInUrfu(long id, AuthenticateInUrfuRequestModel request)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramChatId == id);
        if (user is null)
            return BadRequest("User not found");

        user.UrfuLogin = request.UrfuLogin;
        user.UrfuPassword = request.UrfuPassword;

        user.IsAuthorizedInUrfu = true; // Доделать
        await repository.SaveChangesAsync();

        //TODO: Логика авторизации

        return Ok();
    }

    [HttpGet("chatId/{id}")]
    public async Task<ActionResult> IsAuthorizedInUrfu(long id)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramChatId == id);
        if (user is null)
            return BadRequest("User not found");

        return user.IsAuthorizedInUrfu ? Ok() : Unauthorized();
    }

    [HttpPatch("chatId/{id}/set/auth")]
    public async Task<ActionResult> SetIsAuthorizedInUrfu(long id, [FromBody] bool isAuthorized)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramChatId == id);
        if (user is null)
            return BadRequest("User not found");

        user.IsAuthorizedInUrfu = isAuthorized;
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("chatId/{id}")]
    public async Task<ActionResult> DeleteUser(long id)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramChatId == id);
        if (user is null)
            return BadRequest("User not found");

        repository.Delete(user);
        await repository.SaveChangesAsync();

        return Ok();
    }
}