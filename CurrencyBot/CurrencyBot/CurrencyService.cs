﻿using CurrencyBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class CurrencyService : ICurrencyService
    {
        public Task<ExchangeRate> GetExchangeRateAsync(ExchangeInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
