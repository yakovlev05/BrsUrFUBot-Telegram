using System.Net;
using BrsTgBot.HttpClients.UserClient.Abstract;
using BrsTgBot.HttpClients.UserClient.Models;

namespace BrsTgBot.HttpClients.UserClient;

public class UserClient : IUserClient
{
    private readonly HttpClient _client;
    private readonly ILogger<UserClient> _logger;

    public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UserClient> logger)
    {
        var url = configuration.GetValue<string>("USER_SERVICE_URL");
        if (url is null) throw new NullReferenceException("USER_SERVICE_URL is not set");

        _client = httpClientFactory.CreateClient("user_client");
        _client.BaseAddress = new Uri(url);

        _logger = logger;
    }

    public async Task RegisterInBotAsync(int userId, string username, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegisterInBotAsync with userId: {userId} and username: {username}", userId, username);

        var model = new AddUserRequestModel(null, null, userId, username);
        var response = await _client.PostAsJsonAsync("add", model, cancellationToken);

        // Игнорируем 400, потому что нас не интересует регистрация пользователя
        if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
            throw new HttpRequestException($"Failed to register user in bot. Status code: {response.StatusCode}");
    }
}