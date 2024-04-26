namespace CurrencyBot.Data
{
    public class ExchangeRate : IEquatable<ExchangeRate>
    {
        public string BaseCurrency { get; set; }
        public string Currency { get; set; }
        public decimal SaleRateNB { get; set; }
        public decimal PurchaseRateNB { get; set; }
        public decimal SaleRate { get; set; }
        public decimal PurchaseRate { get; set; }

        public ExchangeRate()
        {
            
        }

        public ExchangeRate(string baseCurrency, string currency, decimal saleNB, decimal purchaseNB, decimal sale, decimal purchase)
        {
            BaseCurrency = baseCurrency;
            Currency = currency;
            SaleRateNB = saleNB;
            PurchaseRateNB = purchaseNB;
            SaleRate = sale;
            PurchaseRate = purchase;
        }

        public bool Equals(ExchangeRate other)
        {
            return Currency == other.Currency && PurchaseRate == other.PurchaseRate && SaleRate == other.SaleRate && SaleRateNB == other.SaleRateNB;
        }
    }
}
