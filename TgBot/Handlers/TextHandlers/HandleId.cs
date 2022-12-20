using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Handlers.TextHandlers;

public class HandleId : ITextHandler
{
    public async Task SendMessage(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        if (message.From is null)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ошибка при обработке сообщения",
                cancellationToken: cancellationToken);
            return;
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Ваш ID - {message.From.Id}",
            cancellationToken: cancellationToken);
    }
}