using Microsoft.AspNetCore.Mvc;
using UserService.Data.Entities;
using UserService.Data.Repositories;
using UserService.Models.UserController;

namespace UserService.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IRepository<UserEntity> repository) : Controller
{
    [HttpPost]
    public async Task<ActionResult> AddUser(AddUserRequest request)
    {
        await repository.AddAsync(new UserEntity()
        {
            TelegramId = request.TelegramId,
            TelegramUsername = request.TelegramUsername
        });

        return Ok();
    }

    [HttpGet]
    public ActionResult<string> Test()
    {
        Console.WriteLine("Эндпоинт тест");
        return "подключение есть";
    }
}