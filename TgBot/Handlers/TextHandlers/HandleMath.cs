using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handlers.TextHandlers;

public class HandleMath : ITextHandler
{
    public async Task SendMessage(ITelegramBotClient botClient, Message? message,
        CancellationToken cancellationToken)
    {
        var chatId = message?.Chat.Id;

        if (chatId is null)
        {
            return;
        }
        
        var strings = MathOperationsUtils.DoOperationsV2(message);

        if (strings.Count is 0)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Не получилось распарсить пример!",
                cancellationToken: cancellationToken);
        }
        else
        {
            var empty = strings.Count > 1 ? "ы" : "";
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Результат{empty} вычислений:\n{string.Join('\n', strings)}",
                cancellationToken: cancellationToken);
        }
        
    }
}