namespace BrsTgBot.HttpClients.UserClient.Models;

public record RegisterUserRequestModel(long TelegramChatId, string? TelegramUsername);