using CurrencyBot.Data;
using CurrencyBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool actual;
            var date = DateOnly.Parse(dateString);

            actual = _bankValidator.VilidateDate(new ExchangeInfo(date, ""));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("first", true)]
        [DataRow("dasd", false)]
        [DataRow("third", true)]
        public void ValidateCurrencyCode_IsCorrectValidation(string code, bool expected)
        {
            ExchangeInfo info = new(new DateOnly(), code);
            List<ExchangeRate> rates = [new ExchangeRate() { Currency = "first" }, new ExchangeRate() { Currency = "second"}, new ExchangeRate() { Currency= "third"}];
            ExchangeRateList rateList = new ExchangeRateList() { ExchangeRate = rates };
            bool actual;

            actual = _bankValidator.VilidateCurrencyCode(rateList, info);

            Assert.AreEqual(expected, actual);
        }
    }
}
