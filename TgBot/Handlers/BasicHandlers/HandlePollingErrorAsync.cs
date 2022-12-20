using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace TgBot.Handlers.BasicHandlers;

public partial class Handlers
{
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        // тут можно допилить что-нибудь например запись в логи а не в консоль

        string errorMessage;

        if (exception is ApiRequestException apiRequestException)
        {
            errorMessage = $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n" +
                           $"{apiRequestException.Message}";
        }
        else
        {
            errorMessage = exception.ToString();
        }

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}