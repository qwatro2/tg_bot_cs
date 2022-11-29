using DeepMorphy.Model;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Utils;

public static class MorphAnalyzer
{

    private static readonly string[] HelloWords = {
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
    private static readonly string[] AfterKind = {
        "утро",
        "день",
        "вечер",
        "ночь"
    };
    private static readonly string[] IdWords = {
        "айди",
        "id",
        "айдишник"
    };
    private static readonly string[] MathWords = {
        "посчитать",
        "найти",
        "решать",
        "реша"
    };
    private static readonly string[] CurrencyWords = {
        "курс",
        "валюта",
        "кэш",
        "лавэ",
        "деньги",
        "доллар",
        "евро",
        "юань",
        "рубль"
    };
    private static DeepMorphy.MorphAnalyzer _morphAnalyzer = new (withLemmatization: true);
    
    public static Task<Message> DoThisShit(ITelegramBotClient botClient, Message message,
        CancellationToken cts)
    {
        var wordsBase = message.Text.Split()
            .Select(x => string.Concat(x.Select(c => char.IsLetter(c) ? c.ToString() : "")))
            .ToArray();

        var words = new List<string>();
        foreach (var word in wordsBase)
        {
            try
            {
                words.Add(_morphAnalyzer.Parse(new[] { word }).ToArray()[0].BestTag.Lemma);
            }
            catch
            {
                continue;
            }
        }

        var (helloFlag,
            idFlag,
            mathFlag,
            currencyFlag) = (false, false, false, false);

        for (var i = 0; i < words.Count; i++)
        {
                if (HelloWords.Contains(words[i]))
                {
                    if (words[i] is "добрый" && i + 1 < words.Count && AfterKind.Contains(words[i + 1]))
                    {
                        helloFlag = true;
                    }
                    else if (words[i] is not "добрый")
                    {
                        helloFlag = true;
                    }
                }

                if (IdWords.Contains(words[i]))
                {
                    idFlag = true;
                }
                
                if (MathWords.Contains(words[i]))
                {
                    mathFlag = true;
                }
                
                if (CurrencyWords.Contains(words[i]))
                {
                    currencyFlag = true;
                }
        }
        
        // TODO: remove debug
        return botClient.SendTextMessageAsync(message.Chat.Id, 
            String.Join('\n', words),
            cancellationToken: cts);
    }
}