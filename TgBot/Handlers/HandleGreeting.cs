using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Handlers;

public partial class Handlers
{
    private static readonly string[] HelloWords =
    {
        "привет",
        "здаров",
        "здоров",
        "здравствуй",
        "здравствуйте",
        "добрый",
        "приветствовать",
        "салют",
        "приветик",
        "хэлло",
        "хай"
    };

    private async Task HandleGreetings(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        var greeting = HelloWords[new Random().Next(HelloWords.Length)];

        if (greeting is "приветствовать")
        {
            greeting = "приветствую";
        }
        else if (greeting[^3..] is "ров")
        {
            greeting += "a";
        }

        if (message.From is null)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ошибка при обработке сообщения",
                cancellationToken: cancellationToken);
            return;
        }
        
        await botClient.SendTextMessageAsync(chatId,
            $"{char.ToUpper(greeting[0]) + greeting[1..]}, " +
            $"{message.From.FirstName}",
            cancellationToken: cancellationToken);
    }
}