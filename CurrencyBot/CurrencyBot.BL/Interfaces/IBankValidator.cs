using CurrencyBot.Data;

namespace CurrencyBot.BL.Interfaces
{
    public interface IBankValidator
    {
        public bool ValidateDate(ExchangeInfo exchangeInfo);

        public bool ValidateCurrencyCode(ExchangeRateList list, ExchangeInfo info);

        public bool ValidateBankResponse(HttpResponseMessage response);
    }
}
