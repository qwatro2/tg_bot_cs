using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Handlers;

public partial class Handlers
{
    private async Task HandleId(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Ваш ID - {message.From.Id}",
            cancellationToken: cancellationToken);
    }
}