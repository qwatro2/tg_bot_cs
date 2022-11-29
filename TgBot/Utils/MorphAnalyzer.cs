using DeepMorphy.Model;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Utils;

public static class MorphAnalyzer
{

    private static string[] _helloWords = new[]
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

    private static string[] _afterKind = new[]
    {
        "утро",
        "день",
        "вечер",
        "ночь"
    };

    private static DeepMorphy.MorphAnalyzer _morphAnalyzer = new (withLemmatization: true);
    
    public static Task<Message> DoThisShit(ITelegramBotClient botClient, Message message,
        CancellationToken cts)
    {
        var words = message.Text.Split()
            .Select(x => string.Concat(x.Select(c => char.IsLetter(c) ? c.ToString() : "")))
            .ToArray();
        
        try
        {
            words = _morphAnalyzer.Parse(words).Select(x => x.BestTag.Lemma).ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return botClient.SendTextMessageAsync(message.Chat.Id, $"Bad words", cancellationToken: cts);
        }

        var (helloFlag,
            idFlag,
            mathFlag,
            currencyFlag) = (false, false, false, false);

        for (var i = 0; i < words.Length; i++)
        {
                if (_helloWords.Contains(words[i]))
                {
                    if (words[i] is "добрый" && i + 1 < words.Length && _afterKind.Contains(words[i + 1]))
                    {
                        helloFlag = true;
                    }
                    else if (words[i] is not "добрый")
                    {
                        helloFlag = true;
                    }
                }
        }

        return botClient.SendTextMessageAsync(message.Chat.Id, "пошел нахуй", cancellationToken: cts);
    }
}