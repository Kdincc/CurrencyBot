namespace CurrencyBot.BL.Interfaces
{
    public interface IBankErrorHandler : IObserver<string>
    {
        public bool IsErrorHandled { get; }
        public string ErrorMessage { get; }
    }
}
