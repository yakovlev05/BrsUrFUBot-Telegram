namespace BrsTgBot.HttpClients.UserClient.Models;

public record AddUserRequestModel(
    string? UrfuLogin,
    string? UrfuPassword,
    int TelegramId,
    string TelegramUsername);