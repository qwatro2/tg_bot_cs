using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handlers;

public partial class Handlers
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var message = update.Message;

        if (update.Message.From.IsBot)
        {
            return;
        }

        HashSet<OperationFlag> operationFlags = MorphAnalyzer.GetOperations(message);

        foreach (var operationFlag in operationFlags)
        {
            switch (operationFlag)
            {
                case OperationFlag.HelloFlag:
                    await HandleGreetings(botClient, message, cancellationToken);
                    break;
                case OperationFlag.IdFlag:
                    await HandleId(botClient, message, cancellationToken);
                    break;
                case OperationFlag.MathFlag:
                    await HandleMath(botClient, message, cancellationToken);
                    break;
                case OperationFlag.CurrencyFlag:
                    await HandleCurrency(botClient, message, cancellationToken);
                    break;
                default:
                    return;
            }
        }
    }
}