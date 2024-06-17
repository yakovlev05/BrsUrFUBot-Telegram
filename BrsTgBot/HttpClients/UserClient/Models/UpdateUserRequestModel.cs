namespace BrsTgBot.HttpClients.UserClient.Models;

public record UpdateUserRequestModel(
    string? UrfuLogin,
    string? UrfuPassword,
    int TelegramId,
    string TelegramUsername);