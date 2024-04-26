using CurrencyBot.BL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace CurrencyBot;

public class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var services = new ServiceCollection().RegisterServices().RegisterSettings(configuration).RegisterClients(configuration);
        using var provider = services.BuildServiceProvider();

        IUpdateHandler updateHandler = provider.GetRequiredService<IUpdateHandler>();
        ITelegramBotFactory telegramBotFactory = provider.GetRequiredService<ITelegramBotFactory>();
        ITelegramBotClient telegramBotClient = telegramBotFactory.GetBot();

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
