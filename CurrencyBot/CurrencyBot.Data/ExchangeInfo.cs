namespace CurrencyBot.Data
{
    public class ExchangeInfo(DateOnly date, string currencyCode)
    {
        private readonly DateOnly _date = date;
        private readonly string _currenceCode = currencyCode;

        public DateOnly Date => _date;
        public string CurrencyCode => _currenceCode;
    }
}
