using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace TgBot;

static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Bot starting...");
        var token = Environment.GetEnvironmentVariable("token") ?? throw new Exception("Token missing");
        var client = new TelegramBotClient(token);
        var handlers = new Handlers.Handlers();

        using var cts = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        Console.WriteLine("Bot was started!");
            
        await client.ReceiveAsync(updateHandler: handlers.HandleUpdateAsync,
            pollingErrorHandler: handlers.HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token);

        cts.Cancel();
    }
}