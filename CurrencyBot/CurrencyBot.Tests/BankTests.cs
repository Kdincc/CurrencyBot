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

        public BankTests()
        {
            validatorMock = new();
            apiHelperMock = new();
            mockResponseMessage = new();
            bank = new Bank(validatorMock.Object, apiHelperMock.Object);
        }

        [TestMethod]
        [DataRow("USD", "02.03.2023", 38.400, 38.900, 36.5686)]
        public void GetExhangeRate_CorrectData(string expectedCode, string date, double expectedPurchase, double expectedSale, double nationalRate)
        {
            //arrange
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

            //setup
            validatorMock.Setup(m => m.VilidateDate(info)).Returns(true);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.VilidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(true);
            apiHelperMock.Setup(m => m.GetBankResponseByDateAsync(DateOnly.Parse(date)))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText("\\Task9\\CurrencyBot\\CurrencyBot.Tests\\TestRate.json"))
                });

            //act
            actual = bank.GetExchangeRateAsync(info).Result;

            //assert
            Assert.AreEqual(expected, actual);
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

            //setup
            validatorMock.Setup(m => m.VilidateDate(info)).Returns(validateDateResult);
            validatorMock.Setup(m => m.ValidateBankResponse(It.IsAny<HttpResponseMessage>())).Returns(true);
            validatorMock.Setup(m => m.VilidateCurrencyCode(It.IsAny<ExchangeRateList>(), info)).Returns(validateCodeResult);
            apiHelperMock.Setup(m => m.GetBankResponseByDateAsync(DateOnly.Parse(date)))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText("\\Task9\\CurrencyBot\\CurrencyBot.Tests\\TestRate.json"))
                });

            //act
            actual = bank.GetExchangeRateAsync(info).Result;

            //assert
            Assert.AreEqual(expected, actual);
        }

    }
}
