using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;
using TgBot.Handlers.TextHandlers;

namespace TgBot.Handlers.BasicHandlers;

public partial class Handlers
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var message = update.Message;

        if (message?.From is null)
        {
            return;
        }

        if (message.From.IsBot)
        {
            return;
        }

        var operationFlags = MorphAnalyzer.GetOperations(message);

        foreach (var operationFlag in operationFlags)
        {
            await operationFlag.SendMessage(botClient, message, cancellationToken);
        }
    }
}