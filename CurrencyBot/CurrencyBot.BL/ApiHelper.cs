using CurrencyBot.BL.Interfaces;

namespace CurrencyBot.BL
{
    public class ApiHelper(IHttpClientFactory factory) : IApiHelper
    {
        private readonly IHttpClientFactory _factory = factory;

        public async Task<HttpResponseMessage> GetBankResponseByDateAsync(DateOnly date)
        {
            string queryString = $"?json&date={date}";

            var client = _factory.CreateClient("BankApi");

            HttpResponseMessage message = await client.GetAsync(queryString);


            return message;
        }
    }
}
