using BrsTgBot.HttpClients.Abstract;

namespace BrsTgBot.HttpClients;

public class UserClient : IUserClient
{
    private readonly HttpClient _client;

    public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        var url = configuration.GetValue<string>("USER_SERVICE_URL");
        if (url is null) throw new NullReferenceException("USER_SERVICE_URL is not set");

        _client = httpClientFactory.CreateClient("user_client");
        _client.BaseAddress = new Uri(url);
    }

    public async Task TestConnection()
    {
        var resp = await _client.GetAsync("/user");
        var t = await resp.Content.ReadAsStringAsync();
        Console.WriteLine(t);
        Console.WriteLine(resp.StatusCode);
    }
}