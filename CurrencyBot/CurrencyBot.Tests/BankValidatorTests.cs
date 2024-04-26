using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;
using Moq;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class BankValidatorTests
    {
        private readonly IBankValidator _bankValidator;
        private readonly Mock<TimeProvider> _timeProviderMock = new();

        public BankValidatorTests()
        {
            _bankValidator = new BankValidator(_timeProviderMock.Object);

            _timeProviderMock.Setup(m => m.LocalTimeZone).Returns(TimeZoneInfo.Local);
            _timeProviderMock.Setup(m => m.GetUtcNow()).Returns(DateTimeOffset.UtcNow);
        }

        [TestMethod]
        [DataRow("2004-03-12", false)]
        [DataRow("2024-03-12", true)]
        [DataRow("2034-03-12", false)]
        public void ValidateDate_IsCorrectValidation(string dateString, bool expected)
        {
            //arrange
            bool actual;
            var date = DateOnly.Parse(dateString);

            //assert
            actual = _bankValidator.ValidateDate(new ExchangeInfo(date, ""));

            //act
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("first", true)]
        [DataRow("dasd", false)]
        [DataRow("third", true)]
        public void ValidateCurrencyCode_IsCorrectValidation(string code, bool expected)
        {
            //arrange
            ExchangeInfo info = new(new DateOnly(), code);
            List<ExchangeRate> rates = [new ExchangeRate() { Currency = "first" }, new ExchangeRate() { Currency = "second" }, new ExchangeRate() { Currency = "third" }];
            ExchangeRateList rateList = new() { ExchangeRate = rates };
            bool actual;

            //act
            actual = _bankValidator.ValidateCurrencyCode(rateList, info);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
