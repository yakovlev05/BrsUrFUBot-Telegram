namespace UserService.Models.UserController;

public record UserResponseModel(
    int Id,
    string? UrfuLogin,
    string? UrfuPassword,
    DateTime RegisteredInBot,
    int TelegramId,
    string TelegramUsername);