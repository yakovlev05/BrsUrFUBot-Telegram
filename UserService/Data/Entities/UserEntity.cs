namespace UserService.Data.Entities;

public class UserEntity
{
    public int Id { get; init; }
    public string? UrfuLogin { get; set; }
    public string? UrfuPassword { get; set; }
    public bool IsAuthorizedInUrfu { get; set; }
    public DateTime RegisteredInBot = DateTime.Now.ToUniversalTime();
    public long TelegramChatId { get; set; }
    public string TelegramUsername { get; set; } = null!;
}