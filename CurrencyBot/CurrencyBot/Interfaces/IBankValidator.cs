using CurrencyBot.Data;

namespace CurrencyBot.Interfaces
{
    public interface IBankValidator
    {
        public bool VilidateDate(ExchangeInfo exchangeInfo);

        public bool VilidateCurrencyCode(ExchangeRateList list, ExchangeInfo info);

        public bool ValidateBankResponse(HttpResponseMessage response);
    }
}
