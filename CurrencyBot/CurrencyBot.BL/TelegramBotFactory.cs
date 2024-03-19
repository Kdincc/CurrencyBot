using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot;

namespace CurrencyBot.BL
{
    public class TelegramBotFactory(IOptions<BotSettings> options) : ITelegramBotFactory
    {
        private readonly IOptions<BotSettings> _options = options;

        public ITelegramBotClient GetBot()
        {
            TelegramBotClient telegramBotClient = new(_options.Value.BotToken);

            telegramBotClient.SetMyDescriptionAsync("This bot can give you the exchange rate of UAH to different currincies using Privat24, to start using bot enter the currency code and date");

            return telegramBotClient;
        }
    }
}
