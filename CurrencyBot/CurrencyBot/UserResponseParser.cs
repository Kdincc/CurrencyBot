﻿using CurrencyBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class UserResponseParser : IUserResponseParser
    {
        public ParsingResults<ExchangeInfo> ParseExchangeInfo(string response)
        {
            Regex regex = new(@"code: ...\, date: \d\d\.\d\d\.\d\d\d\d");
            string lowerResponse = response.ToLower();

            if (regex.IsMatch(lowerResponse))
            {
                var parametres = lowerResponse.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var code = GetParametrValue(parametres[0]);
                var dateString = GetParametrValue(parametres[1]);

                if (DateOnly.TryParse(dateString, out DateOnly date))
                {
                    ExchangeInfo info = new(date, code.ToUpper());

                    return new ParsingResults<ExchangeInfo>(info, true);
                }
            }

            return new ParsingResults<ExchangeInfo>(new ExchangeInfo(new DateOnly(), string.Empty), false);
        }

        private string GetParametrValue(string parametr)
        {
            int cutIndex = parametr.IndexOf(':') + 1;

            return parametr[cutIndex ..];
        }
    }
}
