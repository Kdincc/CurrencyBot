using CurrencyBot.Data;

namespace CurrencyBot.Interfaces
{
    public interface IUserResponseParser
    {
        public ParsingResults<ExchangeInfo> ParseExchangeInfo(string response);
    }
}
