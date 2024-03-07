using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot.Interfaces
{
    public interface IUserResponseParser
    {
        public bool IsUserResponseCorrect(string response);
    }
}
