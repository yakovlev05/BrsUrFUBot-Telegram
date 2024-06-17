namespace UserService.Models.UserController;

public record AddRequest(
    string? UrfuLogin,
    string? UrfuPassword,
    int TelegramId,
    string TelegramUsername);