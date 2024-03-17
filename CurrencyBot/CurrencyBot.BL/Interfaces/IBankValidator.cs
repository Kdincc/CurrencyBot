using CurrencyBot.Data;

namespace CurrencyBot.BL.Interfaces
{
    public interface IBankValidator
    {
        public bool VilidateDate(ExchangeInfo exchangeInfo);

        public bool VilidateCurrencyCode(ExchangeRateList list, ExchangeInfo info);

        public bool ValidateBankResponse(HttpResponseMessage response);
    }
}
