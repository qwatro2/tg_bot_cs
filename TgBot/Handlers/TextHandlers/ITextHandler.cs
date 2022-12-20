using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Handlers.TextHandlers;

public interface ITextHandler
{
    public Task SendMessage(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken);
}