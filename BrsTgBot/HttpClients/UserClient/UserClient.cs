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

    public async Task RegisterUserAsync(long chatId, string? username, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user: {cahtId}", chatId);
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

    public async Task<bool> AuthenticateInUrfuAsync(long chatId, string urfuLogin, string urfuPassword,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Authenticating user in Urfu: {ChatId}", chatId);
        var request = new AuthenticateInUrfuRequestModel(urfuLogin, urfuPassword);

        try
        {
            var response = await _client.PatchAsJsonAsync($"/user/chatId/{chatId}", request, cancellationToken);
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Unauthorized)
            {
                await _telegramService.SendErrorMessageAsync(chatId,
                    $"Error while authenticating user in Urfu, status code: {response.StatusCode}", cancellationToken);
                return false;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized) return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while authenticating user in Urfu");
            await _telegramService.SendErrorMessageAsync(chatId, e.ToString(), cancellationToken);
            return false;
        }

        return true;
    }

    public async Task<bool> IsAuthorizedInUrfuAsync(long chatId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking if user is authorized in Urfu: {ChatId}", chatId);

        try
        {
            var response = await _client.GetAsync($"/user/chatId/{chatId}", cancellationToken);
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Unauthorized)
            {
                await _telegramService.SendErrorMessageAsync(chatId,
                    $"Error while checking if user is authorized in Urfu, status code: {response.StatusCode}",
                    cancellationToken);
                return false;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized) return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while checking if user is authorized in Urfu");
            await _telegramService.SendErrorMessageAsync(chatId, e.ToString(), cancellationToken);
            return false;
        }

        return true;
    }
}