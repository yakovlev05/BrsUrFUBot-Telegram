using Microsoft.AspNetCore.Mvc;
using UserService.Data.Entities;
using UserService.Data.Repositories;
using UserService.Models.UserController;

namespace UserService.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IRepository<UserEntity> repository) : Controller
{
    [HttpPost("add")]
    public async Task<ActionResult> Add([FromBody] AddRequest request)
    {
        var existingUser = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramId == request.TelegramId);
        if (existingUser is not null) return BadRequest("User already exists");

        await repository.AddAsync(new UserEntity()
        {
            TelegramId = request.TelegramId,
            TelegramUsername = request.TelegramUsername,
            UrfuLogin = request.UrfuLogin,
            UrfuPassword = request.UrfuPassword
        });

        return Ok();
    }

    [HttpGet("telegramId/{id}")]
    public async Task<ActionResult<UserResponseModel>> GetByTelegramId(int id)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramId == id);
        if (user is null) return NotFound("User not found");

        return new UserResponseModel(
            user.Id,
            user.UrfuLogin,
            user.UrfuPassword,
            user.RegisteredInBot,
            user.TelegramId, user.TelegramUsername);
    }

    [HttpPut("telegramId/{id}")]
    public async Task<ActionResult> UpdateByTelegramId(int id, UpdateUserRequestModel request)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramId == id);
        if (user is null) return NotFound("User not found");

        user.UrfuLogin = request.UrfuLogin;
        user.UrfuPassword = request.UrfuPassword;
        user.TelegramId = request.TelegramId;
        user.TelegramUsername = request.TelegramUsername;

        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("telegramId/{id}")]
    public async Task<ActionResult> DeleteByTelegramId(int id)
    {
        var user = (await repository.GetAllAsync()).FirstOrDefault(x => x.TelegramId == id);
        if (user is null) return NotFound("User not found");

        repository.Delete(user);
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public ActionResult<string> Test()
    {
        Console.WriteLine("Эндпоинт тест");
        return "подключение есть";
    }
}