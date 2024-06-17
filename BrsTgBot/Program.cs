using BrsTgBot.HttpClients;
using BrsTgBot.HttpClients.Abstract;
using BrsTgBot.Services;
using BrsTgBot.Services.Interfaces;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        var botToken = builder.Configuration.GetValue<string>("BOT_TOKEN");
        if (botToken is null) throw new NullReferenceException("BOT_TOKEN is not set.");
        TelegramBotClientOptions options = new(botToken);
        return new TelegramBotClient(options, httpClient);
    });
builder.Services.AddHostedService<WebhookService>();
builder.Services.AddScoped<IUpdateHandlers, UpdateHandlers>();
builder.Services.AddScoped<IUpdateHandler<MessageUpdateHandler>, MessageUpdateHandler>();
builder.Services.AddScoped<IUpdateHandler<CallbackQueryUpdateHandler>, CallbackQueryUpdateHandler>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<IUserClient, UserClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();