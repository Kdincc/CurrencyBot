using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot.Interfaces
{
    public interface IBankValidator
    {
        public bool VilidateDate(ExchangeInfo exchangeInfo);

        public bool VilidateCurrencyCode(ExchangeRateList list, ExchangeInfo info);

        public bool ValidateBankResponse(HttpResponseMessage response);
    }
}
