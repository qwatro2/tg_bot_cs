using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot;

class Program
{
    public static async Task Main()
    {
        var token = Environment.GetEnvironmentVariable("token") ?? throw new Exception("Token missing");
        var client = new TelegramBotClient(token);
        var handlers = new Handlers();

        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
            
        client.StartReceiving(updateHandler: handlers.HandleUpdateAsync,
            pollingErrorHandler: handlers.HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token);

        var me = await client.GetMeAsync(cancellationToken: cts.Token);
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        cts.Cancel();
    }
}