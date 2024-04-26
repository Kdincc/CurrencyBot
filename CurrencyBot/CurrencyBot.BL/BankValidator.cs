using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;

namespace CurrencyBot.BL
{
    public class BankValidator(TimeProvider timeProvider) : IBankValidator
    {
        private readonly TimeProvider _timeProvider = timeProvider;

        public bool ValidateBankResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool ValidateCurrencyCode(ExchangeRateList list, ExchangeInfo info)
        {
            if (list.ExchangeRate is null || !list.ExchangeRate.Any(e => e.Currency == info.CurrencyCode))
            {
                return false;
            }

            return true;
        }

        public bool ValidateDate(ExchangeInfo info)
        {
            int possibleYearDiff = 4;
            bool isDateNotActual = DateOnly.FromDateTime(_timeProvider.GetLocalNow().Date) < info.Date;
            bool isDateOlderThanDiff = info.Date.Year < _timeProvider.GetLocalNow().Year - possibleYearDiff;

            if (isDateOlderThanDiff || isDateNotActual)
            {
                return false;
            }

            return true;
        }
    }
}
