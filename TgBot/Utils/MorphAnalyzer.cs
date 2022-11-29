using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Utils;

public static class MorphAnalyzer
{
    public static Task<Message> DoThisShit(ITelegramBotClient botClient, Message message,
        CancellationToken cts)
    {
        var words = message.Text.Split()
            .Select(x => string.Concat(x.Select(c => char.IsLetter(c) ? c.ToString() : "")));
        
        return botClient.SendTextMessageAsync(message.Chat.Id, string.Join('\n', words), cancellationToken: cts);
    }
}