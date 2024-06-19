namespace BrsTgBot.HttpClients.UserClient.Abstract;

public interface IUserClient
{
    public Task RegisterUserAsync(long chatId, string? username, CancellationToken cancellationToken);

    public Task<bool> AuthenticateInUrfuAsync(long chatId, string urfuLogin, string urfuPassword,
        CancellationToken cancellationToken);

    public Task<bool> IsAuthorizedInUrfuAsync(long chatId, CancellationToken cancellationToken);
}