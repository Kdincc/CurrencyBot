﻿namespace CurrencyBot.Data
{
    public class ParsingResults<T>(T result, bool isValid)
    {
        private readonly T _result = result;
        private readonly bool _isValid = isValid;


        public T Result => _result;

        public bool IsValid => _isValid;
    }
}
