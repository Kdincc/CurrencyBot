using CurrencyBot.BL;
using CurrencyBot.BL.Interfaces;
using CurrencyBot.Data;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class BankValidatorTests
    {
        private readonly IBankValidator _bankValidator = new BankValidator();

        [TestMethod]
        [DataRow("12.03.2004", false)]
        [DataRow("12.03.2024", true)]
        [DataRow("12.03.2034", false)]
        public void ValidateDate_IsCorrectValidation(string dateString, bool expected)
        {
            //arrange
            bool actual;
            var date = DateOnly.Parse(dateString);

            //assert
            actual = _bankValidator.VilidateDate(new ExchangeInfo(date, ""));

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
            List<ExchangeRate> rates = [new ExchangeRate() { Currency = "first" }, new ExchangeRate() { Currency = "second"}, new ExchangeRate() { Currency= "third"}];
            ExchangeRateList rateList = new() { ExchangeRate = rates };
            bool actual;

            //act
            actual = _bankValidator.VilidateCurrencyCode(rateList, info);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
