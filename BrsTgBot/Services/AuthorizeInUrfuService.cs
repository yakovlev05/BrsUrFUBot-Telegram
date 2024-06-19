using BrsTgBot.HttpClients.UserClient.Abstract;
using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BrsTgBot.Services;

public class AuthorizeInUrfuService(ITelegramBotClient botClient, IUserClient userClient) : IAuthorizeInUrfuService
{
    public async Task<bool> EnsureAuthorizedInUrfuAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = GetChatId(update);
        var username = GetUsername(update);

        if (chatId == long.MinValue) return false; // В этот момент мы заблокированы(одна из причин)

        if (await userClient.IsAuthorizedInUrfuAsync(chatId, cancellationToken))
            return true; // Пользователь авторизован

        await userClient.RegisterUserAsync(chatId, username, cancellationToken);
        return await ValidateLoginAndPasswordAsync(chatId, update, cancellationToken);
    }

    private long GetChatId(Update update)
    {
        var chatId = update.Type switch
        {
            UpdateType.Message => update.Message?.Chat.Id,
            UpdateType.CallbackQuery => update.CallbackQuery?.Message?.Chat.Id,
            _ => long.MinValue
        };

        return chatId.Value;
    }

    private string? GetUsername(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => update.Message?.Chat.Username,
            UpdateType.CallbackQuery => update.CallbackQuery?.Message?.Chat.Username,
            _ => null
        };
    }

    private string[]? GetLoginAndPassword(Update update)
    {
        if (update.Message?.Text is null) return null;

        var loginAndPassword = update.Message.Text.Split('\n');

        return loginAndPassword;
    }

    private async Task<bool> ValidateLoginAndPasswordAsync(long chatId, Update update,
        CancellationToken cancellationToken)
    {
        var loginPassword = GetLoginAndPassword(update);
        if (loginPassword?.First() == "/start")
        {
            await SendInfoAuthenticationAsync(chatId, cancellationToken);
            return false;
        }

        if (loginPassword is null || loginPassword.Length != 2)
        {
            await DeletePreviousMessageAsync(chatId, update.Message.MessageId, cancellationToken);
            await SendInvalidFormAsync(chatId, cancellationToken);
            return false;
        }

        var result =
            await userClient.AuthenticateInUrfuAsync(chatId, loginPassword[0], loginPassword[1], cancellationToken);

        if (result)
        {
            await DeletePreviousMessageAsync(chatId, update.Message.MessageId, cancellationToken);
            await SendSuccessfulAuthenticationAsync(chatId, cancellationToken);
            return true;
        }
        else
        {
            await DeletePreviousMessageAsync(chatId, update.Message.MessageId, cancellationToken);
            await SendUnsuccessfulAuthenticationAsync(chatId, cancellationToken);
            return false;
        }
    }

    private async Task SendSuccessfulAuthenticationAsync(long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            "✅ Вы успешно авторизовались в УрФУ 🔑",
            cancellationToken: cancellationToken);
    }

    private async Task SendInfoAuthenticationAsync(long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            "⚠️ Для продолжения работы необходимо авторизоваться в УрФУ\nℹ️ Введите логин и пароль по шаблону:\nЛОГИН\nПАРОЛЬ",
            cancellationToken: cancellationToken);
    }

    private async Task SendUnsuccessfulAuthenticationAsync(long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            "❌ Неверный логин или пароль. Попробуйте ещё раз",
            cancellationToken: cancellationToken);
    }

    private async Task SendInvalidFormAsync(long chatId, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId,
            "❌ Неверный формат ввода📝\n \n \nℹ️ Введите логин и пароль по шаблону:\nЛОГИН\nПАРОЛЬ",
            cancellationToken: cancellationToken);
    }

    private async Task DeletePreviousMessageAsync(long chatId, int messageId, CancellationToken cancellationToken)
    {
        await botClient.DeleteMessageAsync(chatId, messageId, cancellationToken);
    }
}