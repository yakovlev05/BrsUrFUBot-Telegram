using BrsTgBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BrsTgBot.Services;

public class TelegramService(ILogger<TelegramService> logger, ITelegramBotClient botClient) : ITelegramService
{
    public async Task SendNewMainMenuAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending main menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("БРС 📊", "brs_menu"),
                InlineKeyboardButton.WithCallbackData("Настройки ⚙️", "settings_menu")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl("О проекте", "https://github.com/yakovlev05/BrsUrFUBot-Telegram")
            }
        });

        await botClient.SendTextMessageAsync(
            message.Chat.Id,
            $@"Добро пожаловать, *{message.Chat.FirstName}*\! Используйте кнопки для навигации\.",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard,
            parseMode: ParseMode.MarkdownV2);
    }

    public async Task SendBrsMenuAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending answer brs menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Обновить 💡", "brs_update"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Главное меню 🚪", "main_menu")
            }
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            "Тест ответ на брс",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);
    }

    public async Task SendSettingsMenuAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending settings menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Обновить данные авторизации 🔑", "settings_update_auth")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Удалить аккаунт 🚩", "settings_remove_account")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Главное меню 🚪", "main_menu"),
            }
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            "Тест ответа настройки",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);
    }

    public async Task SendMainMenuAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending main menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("БРС 📊", "brs_menu"),
                InlineKeyboardButton.WithCallbackData("Настройки ⚙️", "settings_menu")
            },
            new[]
            {
                InlineKeyboardButton.WithUrl("О проекте", "https://github.com/yakovlev05/BrsUrFUBot-Telegram")
            }
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            $@"Добро пожаловать, *{message.Chat.FirstName}*\! Используйте кнопки для навигации\.",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard,
            parseMode: ParseMode.MarkdownV2);
    }

    public async Task SendBrsUpdateAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating brs menu");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Обновить 💡", "brs_update"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Главное меню 🚪", "main_menu")
            }
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            "Брс был обновлён",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);
    }

    public async Task SendSettingsUpdateAuthAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending settings update auth");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            InlineKeyboardButton.WithCallbackData("Отмена ❌", "settings_update_auth_cancel")
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            "Введите новый логин и пароль",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);
    }

    public async Task SendSettingsDeleteAccountQuestionAsync(Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending settings delete account question");

        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Да ✅", "settings_delete_account_question_confirm"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Нет ❌", "settings_delete_account_question_cancel")
            }
        });

        await botClient.EditMessageTextAsync(
            message.Chat.Id,
            message.MessageId,
            "❗Вы точно хотите удалить аккаунт? \n Все ваши данные окончательно будут стёрты с наших серверов💿",
            cancellationToken: cancellationToken,
            replyMarkup: keyboard);
    }

    public async Task SendErrorMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        var baseText = "‼️Произошла ошибка: \n";

        await botClient.SendTextMessageAsync(
            chatId,
            baseText + text,
            cancellationToken: cancellationToken);
    }
}