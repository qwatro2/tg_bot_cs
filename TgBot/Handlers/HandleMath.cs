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
        
        var commandData = message.Text.Split(' ');
        int result = MathOperationsUtils.DoOperations(commandData);
        
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Результат - {result}!",
            cancellationToken: cancellationToken);
    }
}