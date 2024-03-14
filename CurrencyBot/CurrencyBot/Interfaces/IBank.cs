using CurrencyBot.Data;

namespace CurrencyBot.Interfaces
{
    public interface IBank : IObservable<string>
    {
        public Task<ExchangeRate> GetExchangeRateAsync(ExchangeInfo info);
    }
}
