using Telegram.Bot.Types;
using TgBot.Handlers.TextHandlers;

namespace TgBot.Utils;

public enum OperationFlag
{
    HelloFlag,
    IdFlag,
    MathFlag,
    CurrencyFlag
}

public static class MorphAnalyzer
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

    private static readonly string[] AfterKind =
    {
        "утро",
        "день",
        "вечер",
        "ночь"
    };

    private static readonly string[] IdWords =
    {
        "айди",
        "id",
        "айдишник"
    };

    private static readonly string[] MathWords =
    {
        "посчитать",
        "найти",
        "решать",
        "реша"
    };

    private static readonly string[] CurrencyWords =
    {
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

    private static readonly DeepMorphy.MorphAnalyzer StaticMorphAnalyzer = new(withLemmatization: true);

    public static List<ITextHandler> GetOperations(Message? message)
    {
        if (message?.Text is null)
        {
            return new List<ITextHandler>();
        }

        var wordsBase = message.Text.Split()
            .Select(x => string.Concat(x.Select(c => char.IsLetter(c) ? c.ToString() : "")))
            .ToArray();

        var words = new List<string>();
        foreach (var word in wordsBase)
        {
            try
            {
                words.Add(StaticMorphAnalyzer.Parse(new[] { word }).ToArray()[0].BestTag.Lemma);
            }
            catch
            {
                // ignored
            }
        }

        var operationList = new List<ITextHandler>();
        var operationSet = new HashSet<OperationFlag>();

        for (var i = 0; i < words.Count; i++)
        {
            if (HelloWords.Contains(words[i]))
            {
                if (words[i] is "добрый"
                    && i + 1 < words.Count
                    && AfterKind.Contains(words[i + 1])
                    && !operationSet.Contains(OperationFlag.HelloFlag))
                {
                    operationList.Add(new HandleGreeting());
                    operationSet.Add(OperationFlag.HelloFlag);
                }
                else if (words[i] is not "добрый" && !operationSet.Contains(OperationFlag.HelloFlag))
                {
                    operationList.Add(new HandleGreeting());
                    operationSet.Add(OperationFlag.HelloFlag);
                }
            }

            if (IdWords.Contains(words[i]) && !operationSet.Contains(OperationFlag.IdFlag))
            {
                operationList.Add(new HandleId());
                operationSet.Add(OperationFlag.IdFlag);
            }

            if (MathWords.Contains(words[i]) && !operationSet.Contains(OperationFlag.MathFlag))
            {
                operationList.Add(new HandleMath());
                operationSet.Add(OperationFlag.MathFlag);
            }

            if (CurrencyWords.Contains(words[i]) && !operationSet.Contains(OperationFlag.CurrencyFlag))
            {
                operationList.Add(new HandleCurrency());
                operationSet.Add(OperationFlag.CurrencyFlag);
            }
        }

        return operationList;
    }
}