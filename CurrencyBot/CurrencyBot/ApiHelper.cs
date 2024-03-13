namespace CurrencyBot
{
    public static class ApiHelper
    {
        public static async Task<HttpResponseMessage> GetBankResponseByDateAsync(DateOnly date)
        {
            string apiUrl = $"https://api.privatbank.ua/p24api/exchange_rates?json&date={date}";

            using HttpClient client = new();

            HttpResponseMessage message = await client.GetAsync(apiUrl);

            return message;
        }
    }
}
