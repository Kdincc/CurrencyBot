using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace CurrencyBot;

public class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection().RegisterServices();
        using var provider = services.BuildServiceProvider();
        IUpdateHandler updateHandler = provider.GetRequiredService<IUpdateHandler>();
        ITelegramBotClient telegramBotClient = TelegramBotFactory.GetBot();
        CancellationTokenSource source = new();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = [UpdateType.Message]
        };

        telegramBotClient.StartReceiving(updateHandler, receiverOptions);

        Console.ReadLine();

        source.Cancel();
    }
}
