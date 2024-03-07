using CurrencyBot.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<ExchangeRate> GetExchangeRateAsync(ExchangeInfo info)
        {
            string apiUrl = $"https://api.privatbank.ua/p24api/exchange_rates?json&date={info.Date}";
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            string jsonString = await response.Content.ReadAsStringAsync();
            ExchangeRateList exchangeList = JsonConvert.DeserializeObject<ExchangeRateList>(jsonString);

            ExchangeRate rate = exchangeList.ExchangeRates.First(e => e.Currency == info.CurrencyCode);
            
            return rate;
        }
    }
}
