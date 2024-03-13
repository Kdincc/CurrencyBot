using CurrencyBot.Interfaces;

namespace CurrencyBot
{
    public class BankValidator : IBankValidator
    {
        public bool ValidateBankResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool VilidateCurrencyCode(ExchangeRateList list, ExchangeInfo info)
        {
            if (list.ExchangeRate is null || !list.ExchangeRate.Any(e => e.Currency == info.CurrencyCode))
            {
                return false;
            }

            return true;
        }

        public bool VilidateDate(ExchangeInfo info)
        {
            int possibleYearDiff = 4;
            bool isDateNotActual = DateOnly.FromDateTime(DateTime.Now) < info.Date;
            bool isDateOlderThanDiff = info.Date.Year < DateTime.Now.Year - possibleYearDiff;

            if (isDateOlderThanDiff || isDateNotActual)
            {
                return false;
            }

            return true;
        }
    }
}
