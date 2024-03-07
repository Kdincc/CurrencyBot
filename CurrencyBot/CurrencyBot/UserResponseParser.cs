using CurrencyBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class UserResponseParser : IUserResponseParser
    {
        public ParsingResults<ExchangeInfo> ParseExchangeInfo(string response)
        {
            throw new NotImplementedException();
        }
    }
}
