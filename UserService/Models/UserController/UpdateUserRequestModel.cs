namespace UserService.Models.UserController;

public record UpdateUserRequestModel(
    string? UrfuLogin,
    string? UrfuPassword,
    int TelegramId,
    string TelegramUsername);