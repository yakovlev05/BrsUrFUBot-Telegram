using System.Net;
using BrsTgBot.HttpClients.UserClient.Abstract;
using BrsTgBot.HttpClients.UserClient.Models;
using BrsTgBot.Services.Interfaces;

namespace BrsTgBot.HttpClients.UserClient;

public class UserClient : IUserClient
{
    private readonly HttpClient _client;
    private readonly ILogger<UserClient> _logger;
    private readonly ITelegramService _telegramService;

    public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UserClient> logger,
        ITelegramService telegramService)
    {
        var url = configuration.GetValue<string>("USER_SERVICE_URL");
        if (url is null) throw new NullReferenceException("USER_SERVICE_URL is not set");

        _client = httpClientFactory.CreateClient("user_client");
        _client.BaseAddress = new Uri(url);

        _logger = logger;
        _telegramService = telegramService;
    }

    public async Task RegisterUserAsync(long chatId, string username, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user: {Username}", username);
        var request = new RegisterUserRequestModel(chatId, username);

        try
        {
            var response = await _client.PostAsJsonAsync("/user/register", request, cancellationToken);
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.BadRequest)
                await _telegramService.SendErrorMessageAsync(chatId,
                    $"Error while registering user, status code: {response.StatusCode}", cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while registering user");
            await _telegramService.SendErrorMessageAsync(chatId, e.ToString(), cancellationToken);
        }
    }
}