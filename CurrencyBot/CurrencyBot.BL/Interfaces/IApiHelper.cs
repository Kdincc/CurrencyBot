namespace CurrencyBot.BL.Interfaces
{
    public interface IApiHelper
    {
        public Task<HttpResponseMessage> GetBankResponseByDateAsync(DateOnly date);
    }
}
