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

    public async Task<HttpResponseMessage> AddUserAsync(AddUserRequestModel model, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AddUserAsync with model: {model}", model);

        var response = await _client.PostAsJsonAsync("add", model, cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> GetUserByTelegramIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetUserByTelegramIdAsync with id: {id}", id);

        var response = await _client.GetAsync($"telegramId/{id}", cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> UpdateUserByTelegramIdAsync(int id, UpdateUserRequestModel model,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateUserByTelegramIdAsync with id: {id} and model: {model}", id, model);

        var response = await _client.PutAsJsonAsync($"telegramId/{id}", model, cancellationToken);
        return response;
    }

    public async Task<HttpResponseMessage> DeleteUserByTelegramIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteUserByTelegramIdAsync with id: {id}", id);

        var response = await _client.DeleteAsync($"telegramId/{id}", cancellationToken);
        return response;
    }
}