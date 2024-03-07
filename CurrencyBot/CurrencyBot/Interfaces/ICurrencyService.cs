using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot.Interfaces
{
    public interface ICurrencyService
    {
        public Task<ExchangeRate> GetExchangeRateAsync(string cuurencyCode, DateOnly date);
    }
}
