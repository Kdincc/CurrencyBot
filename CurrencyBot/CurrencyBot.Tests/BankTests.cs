using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Moq;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class BankTests
    {
        private readonly Mock<IBankValidator> validatorMock;
        private readonly IBank bank;

        public BankTests()
        {
            validatorMock = new Mock<IBankValidator>();
            bank = new Bank(validatorMock.Object);
        }

        [TestMethod]
        [DataRow("USD", "02.03.2023", 38.400, 38.900, 36.5686)]
        public void GetExhangeRate_CorrectData(string expectedCode, string date, double expectedPurchase, double expectedSale, double nationalRate)
        {
            ExchangeRate expected = new() 
            { 
                Currency = expectedCode, 
                PurchaseRate = (decimal)expectedPurchase,
                SaleRate = (decimal)expectedSale,
                PurchaseRateNB = (decimal)nationalRate,
                SaleRateNB = (decimal)nationalRate
            };
            ExchangeInfo info = new(DateOnly.Parse(date), expectedCode);
            ExchangeRate actual;

            validatorMock.Setup(m => m.VilidateDate(info)).Returns(true);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.VilidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(true);

            actual = bank.GetExchangeRateAsync(info).Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", "02.03.2023", false, true)]
        [DataRow("USD", "10.10.2033", true, false)]
        public void GetExhangeRate_IncorrectData(string expectedCode, string date, bool validateDateResult, bool validateCodeResult)
        {
            ExchangeRate expected = new();
            ExchangeInfo info = new(DateOnly.Parse(date), expectedCode);
            ExchangeRate actual;

            validatorMock.Setup(m => m.VilidateDate(info)).Returns(validateDateResult);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.VilidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(validateCodeResult);

            actual = bank.GetExchangeRateAsync(info).Result;

            Assert.AreEqual(expected, actual);
        }

    }
}
