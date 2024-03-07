using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    public class ParsingResults<T>(T result, bool isValid)
    {
        private readonly T _result = result;
        private readonly bool _isValid = isValid;

        public T Result => _result;

        public bool IsValid => _isValid;
    }
}
