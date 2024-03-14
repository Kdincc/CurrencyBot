using CurrencyBot.Data;
using CurrencyBot.Interfaces;
using System.Text.RegularExpressions;

namespace CurrencyBot
{
    public class UserResponseParser : IUserResponseParser
    {
        public ParsingResults<ExchangeInfo> ParseExchangeInfo(string response)
        {
            Regex regex = new(@"... \d\d\.\d\d\.\d\d\d\d");
            string lowerResponse = response.ToLower();

            if (regex.IsMatch(lowerResponse))
            {
                var parametres = lowerResponse.Split();
                var code = parametres[0];
                var dateString = parametres[1];

                if (DateOnly.TryParse(dateString, out DateOnly date))
                {
                    ExchangeInfo info = new(date, code.ToUpper());

                    return new ParsingResults<ExchangeInfo>(info, true);
                }
            }

            return new ParsingResults<ExchangeInfo>(new ExchangeInfo(new DateOnly(), string.Empty), false);
        }
    }
}
