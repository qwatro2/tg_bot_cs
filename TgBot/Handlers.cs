using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace TgBot;

public class Handlers
{
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } messageText } message)
            return;

        var chatId = message.Chat.Id;

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
    }
    
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

    private void MorphAnalysis(Message message)
    {
        // тут будет анализ сообщения и типо ручки нужные дергаться будут
        throw new NotImplementedException();
    }
    
}