using CurrencyBot.Data;

namespace CurrencyBot.BL.Interfaces
{
    public interface IBank : IObservable<string>
    {
        public Task<ExchangeRate> GetExchangeRateAsync(ExchangeInfo info);
    }
}
