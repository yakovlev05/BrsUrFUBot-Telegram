namespace BrsTgBot.HttpClients.UserClient.Abstract;

public interface IUserClient
{
    public Task RegisterUserAsync(long chatId, string? username, CancellationToken cancellationToken);
}