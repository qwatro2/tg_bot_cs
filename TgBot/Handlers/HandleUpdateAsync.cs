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

        var messageText = update.Message?.Text;
        var chatId = message.Chat.Id;

        var commandData = messageText.Split(' ');
        var command = commandData[0];

        switch (command)
        {
            case "/hello":
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Привет, {message.From.FirstName}!",
                    cancellationToken: cancellationToken);
                break;
            case "/id":
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Ваш ID - {message.From.Id}!",
                    cancellationToken: cancellationToken);
                break;
            case "/math":
                int result = MathOperationsUtils.DoOperations(commandData);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Результат - {result}!",
                    cancellationToken: cancellationToken);
                break;
            case "/currency":
                var responseData = await CurrencyParseUtils.GetAllCurrency();
                string currencyMessage = "Курсы валют:\n";
                foreach (var pair in responseData.Rates)
                {
                    currencyMessage += $"{pair.Key} = {(1 / pair.Value).ToString("F2")}\n";
                }
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: currencyMessage,
                    cancellationToken: cancellationToken);
                break;
            default:
                return;
        }
    }
}