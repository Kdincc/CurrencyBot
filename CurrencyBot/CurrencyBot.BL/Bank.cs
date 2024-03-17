using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Newtonsoft.Json;

namespace CurrencyBot.BL
{
    public class Bank(IBankValidator validator) : IBank
    {
        private readonly List<IObserver<string>> _observers = [];
        private readonly IBankValidator _validator = validator;

        public async Task<ExchangeRate> GetExchangeRateAsync(ExchangeInfo info)
        {
            ExchangeRateList exchangeList = new();
            ExchangeRate exchangeRate = new();

            if (!_validator.VilidateDate(info))
            {
                OnNext("Incorrect date, try check that date must be not older than 4 years");

                return exchangeRate;
            }

            HttpResponseMessage response = await ApiHelper.GetBankResponseByDateAsync(info.Date);

            if (!_validator.ValidateBankResponse(response))
            {
                OnNext("We have some troubles with bank connection, please try later");

                return exchangeRate;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            exchangeList = JsonConvert.DeserializeObject<ExchangeRateList>(jsonString);

            if (!_validator.VilidateCurrencyCode(exchangeList, info))
            {
                OnNext($"Currency with that code {info.CurrencyCode} not found");

                return exchangeRate;
            }

            exchangeRate = exchangeList.ExchangeRate.FirstOrDefault(e => e.Currency == info.CurrencyCode);

            OnComplete();

            return exchangeRate;
        }

        private void OnComplete()
        {
            _observers.ForEach(o => o.OnCompleted());
        }

        private void OnNext(string message)
        {
            _observers.ForEach(o => o.OnNext(message));
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber<string>(_observers, observer);
        }
    }
}
