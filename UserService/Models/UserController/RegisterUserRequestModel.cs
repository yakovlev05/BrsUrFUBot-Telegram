namespace UserService.Models.UserController;

public record RegisterUserRequestModel(long TelegramChatId, string? TelegramUsername);