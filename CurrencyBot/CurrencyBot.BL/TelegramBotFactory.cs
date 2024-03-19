﻿using CurrencyBot.Data;
using Newtonsoft.Json;
using Telegram.Bot;

namespace CurrencyBot.BL
{
    public static class TelegramBotFactory
    {
        public static ITelegramBotClient GetBot()
        {
            string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}appsettings.json";
            var file = JsonConvert.DeserializeObject<ConfigurationFile>(File.ReadAllText(configPath));


            telegramBotClient.SetMyDescriptionAsync("This bot can give you the exchange rate of UAH to different currincies using Privat24, to start using bot enter the currency code and date");

            return telegramBotClient;
        }
    }
}
