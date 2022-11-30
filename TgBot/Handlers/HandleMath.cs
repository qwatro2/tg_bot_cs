using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handlers;

public partial class Handlers
{
    private async Task HandleMath(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        
        var (result, isValid) = MathOperationsUtils.DoOperations(message);

        if (isValid)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Результат вычислений - {result}",
                cancellationToken: cancellationToken);
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Не получилось распарсить пример!",
                cancellationToken: cancellationToken);
        }
        
    }
}