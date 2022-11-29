using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Handlers;

public partial class Handlers
{
    private static string[] _helloWords = {
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
    
    public Task HandleGreetings(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var greeting = _helloWords[new Random().Next(_helloWords.Length)];

        if (greeting is "приветствовать")
        {
            greeting = "приветсвтую";
        }
        else if (greeting[^3..] is "ров")
        {
            greeting = greeting + "a";
        }
            
        return botClient.SendTextMessageAsync(message.Chat.Id, 
            $"{char.ToUpper(greeting[0]) + greeting.Substring(1)}, " +
            $"{message.From.FirstName}",
            cancellationToken: cancellationToken);
    }
}