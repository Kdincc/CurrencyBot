using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot.Interfaces
{
    public interface IBankErrorHandler : IObserver<string>
    {
        public bool IsErrorHandled { get; }
        public string ErrorMessage { get; }
    }
}
