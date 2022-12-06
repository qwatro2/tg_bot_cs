using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Utils;

namespace TgBot.Handlers;

public partial class Handlers
{
    private async Task HandleCurrency(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        
        var responseData = await CurrencyParseUtils.GetAllCurrency();

        if (responseData?.Rates is null)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ошибка при получении курса!",
                cancellationToken: cancellationToken);
            return;
        }
        
        var currencyMessage = responseData.Rates
            .Aggregate("Курсы валют:\n", (current, pair) => current + $"{pair.Key} = {(1 / pair.Value):F2}\n");

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: currencyMessage,
            cancellationToken: cancellationToken);
    }
}