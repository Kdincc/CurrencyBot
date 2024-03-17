using CurrencyBot.Data;

namespace CurrencyBot.BL.Interfaces
{
    public interface IUserResponseParser
    {
        public ParsingResults<ExchangeInfo> ParseExchangeInfo(string response);
    }
}
