using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Moq;
using System.Net;
using System.Text.Json;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class BankTests
    {
        private readonly Mock<IBankValidator> validatorMock;
        private readonly Mock<IApiHelper> apiHelperMock;
        private readonly Mock<HttpResponseMessage> mockResponseMessage;
        private readonly IBank bank;
        private readonly ExchangeRateList exchangeRateListToTest = new();
        public BankTests()
        {
            validatorMock = new();
            apiHelperMock = new();
            mockResponseMessage = new();
            bank = new Bank(validatorMock.Object, apiHelperMock.Object);

            List<ExchangeRate> list =
                [
                    new ExchangeRate("UAH", "PLN", 8.3541m, 8.3541m, 8.84m, 8.3m),
                    new ExchangeRate("UAH", "GBP", 44.1328m, 44.1328m, 46.62m, 43.8m),
                    new ExchangeRate("UAH", "EUR", 39.0754m, 39.0754m, 41m, 40m),
                    new ExchangeRate("UAH", "USD", 36.5686m, 36.5686m, 38.9m, 38.4m)
                ];

            exchangeRateListToTest.ExchangeRate = list;
        }

        [TestMethod]
        [DataRow("USD", "2023-02-03", 38.4, 38.9, 36.5686)]
        public void GetExhangeRate_CorrectData(string expectedCode, string date, double expectedPurchase, double expectedSale, double nationalRate)
        {
            //arrange
            ExchangeRate expected = new()
            {
                BaseCurrency = "UAH",
                Currency = expectedCode,
                PurchaseRate = (decimal)expectedPurchase,
                SaleRate = (decimal)expectedSale,
                PurchaseRateNB = (decimal)nationalRate,
                SaleRateNB = (decimal)nationalRate
            };
            ExchangeInfo info = new(DateOnly.Parse(date), expectedCode);
            ExchangeRate actual;
            File.WriteAllText("TestRate.json", JsonSerializer.Serialize(exchangeRateListToTest));

            //setup
            validatorMock.Setup(m => m.ValidateDate(info)).Returns(true);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.ValidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(true);
            apiHelperMock.Setup(m => m.GetBankResponseByDateAsync(DateOnly.Parse(date)))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText("TestRate.json"))
                });

            //act
            actual = bank.GetExchangeRateAsync(info).Result;

            //assert
            Assert.AreEqual(expected, actual);

            //clean up
            File.Delete("TestRate.json");
        }

        [TestMethod]
        [DataRow("", "02.03.2023", false, true)]
        [DataRow("USD", "10.10.2033", true, false)]
        public void GetExhangeRate_IncorrectData(string expectedCode, string date, bool validateDateResult, bool validateCodeResult)
        {
            //arrange
            ExchangeRate expected = new();
            ExchangeInfo info = new(DateOnly.Parse(date), expectedCode);
            ExchangeRate actual;
            File.WriteAllText("TestRate.json", JsonSerializer.Serialize(exchangeRateListToTest));

            //setup
            validatorMock.Setup(m => m.ValidateDate(info)).Returns(validateDateResult);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.ValidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(validateCodeResult);
            apiHelperMock.Setup(m => m.GetBankResponseByDateAsync(DateOnly.Parse(date)))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText("TestRate.json"))
                });

            //act
            actual = bank.GetExchangeRateAsync(info).Result;

            //assert
            Assert.AreEqual(expected, actual);

            //clean up
            File.Delete("TestRate.json");
        }

    }
}
