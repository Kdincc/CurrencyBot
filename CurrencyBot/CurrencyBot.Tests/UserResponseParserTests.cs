using CurrencyBot.Interfaces;

namespace CurrencyBot.Tests
{
    [TestClass]
    public class UserResponseParserTests
    {
        private readonly IUserResponseParser _parser = new UserResponseParser();

        [TestMethod]
        [DataRow("", false)]
        [DataRow("usd 1.2.2233", false)]
        [DataRow("EUH 01.12.2022", true)]
        public void ParseExchangeRate_IsCorrectValidation(string str, bool expected)
        {
            bool actual;

            actual = _parser.ParseExchangeInfo(str).IsValid;

            Assert.AreEqual(expected, actual);
        }
    }
}