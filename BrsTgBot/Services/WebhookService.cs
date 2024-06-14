using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class WebhookService : IHostedService
{
    private readonly ILogger<WebhookService> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly IConfiguration _configuration;

    public WebhookService(ILogger<WebhookService> logger, ITelegramBotClient botClient, IConfiguration configuration)
    {
        _logger = logger;
        _botClient = botClient;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var webhookUrl = _configuration.GetValue<string>("WEBHOOK_URL");
        if (webhookUrl is null) throw new NullReferenceException("WEBHOOK_URL is not set");
        _logger.LogInformation($"Updating webhook to {webhookUrl}");
        await _botClient.SetWebhookAsync(
            url: webhookUrl,
            allowedUpdates: Array.Empty<UpdateType>(), // Разрешённые обновления, пустой массив - все обновления
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removing webhook");
        await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}