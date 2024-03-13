using CurrencyBot.Interfaces;

namespace CurrencyBot
{
    public class BankErrorHandler : IBankErrorHandler
    {
        private bool _isErrorHandled = false;
        private string _errorMessage = "";

        public bool IsErrorHandled => _isErrorHandled;

        public string ErrorMessage => _errorMessage;

        public void OnCompleted()
        {
            _isErrorHandled = false;
        }

        public void OnError(Exception error)
        {
            _errorMessage = error.Message;
            _isErrorHandled = true;
        }

        public void OnNext(string value)
        {
            _errorMessage = value;
            _isErrorHandled = true;
        }
    }
}
