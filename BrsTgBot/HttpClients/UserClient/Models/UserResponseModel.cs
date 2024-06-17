namespace BrsTgBot.HttpClients.UserClient.Models;

public record UserResponseModel(
    int Id,
    string? UrfuLogin,
    string? UrfuPassword,
    DateTime RegisteredInBot,
    int TelegramId,
    string TelegramUsername);