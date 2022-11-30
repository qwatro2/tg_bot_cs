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
        string currencyMessage = "Курсы валют:\n";
        foreach (var pair in responseData.Rates)
        {
            currencyMessage += $"{pair.Key} = {(1 / pair.Value).ToString("F2")}\n";
        }

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: currencyMessage,
            cancellationToken: cancellationToken);
    }
}