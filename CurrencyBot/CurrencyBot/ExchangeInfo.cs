using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class ExchangeInfo(DateOnly date, string currencyCode)
    {
        private readonly DateOnly _date = date;
        private readonly string _currenceCode = currencyCode;

        public DateOnly Date => _date;
        public string CurrencyCode => _currenceCode;
    }
}
